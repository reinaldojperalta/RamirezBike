using AppRamirezBike.Datos;
using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
    public partial class Checkout : System.Web.UI.Page
    {

        ClOrdenLogica logicaOrden = new ClOrdenLogica();
        ClProductoLogica logicaProducto = new ClProductoLogica();
        ClOrdenDetalleLogica logicaDetalle = new ClOrdenDetalleLogica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    // Obtener la cadena completa: ej. "15,3"
                    string userData = ticket.UserData;

                    // 1. Separar la cadena por la coma
                    string[] dataParts = userData.Split(',');

                    int idUsuario = 0;
                    int idRol = 0;

                    if (dataParts.Length == 2)
                    {
                        // 2. Convertir la primera parte a idUsuario
                        if (int.TryParse(dataParts[0], out idUsuario))
                        {
                            // 3. Convertir la segunda parte a idRol
                            int.TryParse(dataParts[1], out idRol);
                        }
                    }



                    if (idUsuario == 0)
                    {
                        lblMensaje.Text = "Usuario no encontrado";
                        return;
                    }

                    string carritoJson = Session["carrito"] as string;
                    if (string.IsNullOrEmpty(carritoJson))
                    {
                        lblMensaje.Text = "El carrito está vacío";
                        return;
                    }

                    List<ItemCarrito> carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemCarrito>>(carritoJson);

                    // 3) Calcular total leyendo precios desde BD (evitar manipulación cliente)
                    decimal total = 0M;
                    foreach (var item in carrito)
                    {
                        var producto = logicaProducto.ObtenerProductoPorId(item.idProducto);
                        if (producto == null) continue;
                        decimal precio = producto.precio;
                        total += precio * item.cantidad;
                    }
                    string referenciaGenerada = $"RBK-{DateTime.Now.Ticks}";


                    Orden orden = new Orden
                    {
                        IdUsuario = idUsuario,
                        FechaCreacion = DateTime.Now,
                        Estado = "Pendiente",
                        Total = total,
                        MetodoPago = "ePayco",
                        Referencia = referenciaGenerada,
                        FechaPago = null
                    };

                    int idOrden = logicaOrden.CrearOrden(orden);
                    if (idOrden <= 0)
                    {
                        lblMensaje.Text = "Error al crear la orden";
                        return;
                    }


                    foreach (var it in carrito)
                    {
                        var producto = logicaProducto.ObtenerProductoPorId(it.idProducto);
                        if (producto == null) continue;

                        OrdenDetalle detalle = new OrdenDetalle
                        {
                            IdOrden = idOrden,
                            IdProducto = it.idProducto,
                            Cantidad = it.cantidad,
                            PrecioUnitario = producto.precio,
                            Subtotal = producto.precio * it.cantidad
                        };

                        logicaDetalle.RegistrarDetalle(detalle);
                    }

                    // 6) Actualizar total por si acaso (ahora pasamos decimal, no double)
                    logicaOrden.CambiarTotal(idOrden, total);

                    hdnReferencia.Value = referenciaGenerada;
                    // 7) Renderizar valores para JS
                    hdnIdOrden.Value = idOrden.ToString();

                    hdnTotal.Value = total.ToString("F2", CultureInfo.InvariantCulture);
                    lblTotal.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("es-CO"));

                    Session["idOrden"] = idOrden;
                }
            }
        }

        protected void btnIniciarPago_Click(object sender, EventArgs e)
        {
            string publicKey = "f15a645395298a6f8752cfd5364d00c8";

            string idOrden = hdnIdOrden.Value;
            string amount = hdnTotal.Value;
            string referencia = hdnReferencia.Value;

            string script = $@"
        var handler = ePayco.checkout.configure({{ key: '{publicKey}', test: true }});
        handler.open({{
            name: 'Ramirez Bike Store',
            description: 'Compra en linea',
            invoice: '{referencia}',
            currency: 'cop',
            amount: {amount},
            tax_base: '0',
            tax: '0',
            country: 'CO',
            response: 'http://www.RamiresBike.somee.com',
            confirmation: 'http://www.RamiresBike.somee.com'
        }});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenEpayco", script, true);
        }


        public class ItemCarrito
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public double precio { get; set; }
        }
    }
}