using AppRamirezBike.Datos;
using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
    public partial class Checkout : System.Web.UI.Page
    {
        ClUsuarioLogica logicaUsuario = new ClUsuarioLogica();
        ClOrdenLogica logicaOrden = new ClOrdenLogica();
        ClProductoLogica logicaProducto = new ClProductoLogica();
        ClOrdenDetalleLogica logicaDetalle = new ClOrdenDetalleLogica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1) Validar sesión usuario
                if (Session["idUsuario"] == null)
                {
                    Response.Redirect("Login.aspx?msg=Debe iniciar sesión");
                    return;
                }

                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                string correo = Session["emailUsuario"].ToString();
                if (idUsuario == 0)
                {
                    lblMensaje.Text = "Usuario no encontrado";
                    return;
                }

                // 2) Obtener carrito desde Session (llenado en Carrito.aspx)
                string carritoJson = Session["carrito"] as string;
                if (string.IsNullOrEmpty(carritoJson))
                {
                    lblMensaje.Text = "El carrito está vacío";
                    return;
                }

                List<ItemCarrito> carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemCarrito>>(carritoJson);

                // 3) Calcular total leyendo precios desde BD (evitar manipulación cliente)
                decimal total = 0M;
                foreach (var it in carrito)
                {
                    var producto = logicaProducto.ObtenerProductoPorId(it.idProducto);
                    if (producto == null) continue;
                    decimal precio = producto.precio; // asumo decimal
                    total += precio * it.cantidad;
                }
                string referenciaGenerada = $"RBK-{DateTime.Now.Ticks}";

                // 4) Crear orden Pendiente en BD
                Orden orden = new Orden
                {
                    IdUsuario = idUsuario,
                    FechaCreacion = DateTime.Now,
                    Estado = "Pendiente",
                    Total = total,
                    MetodoPago = "ePayco",
                    Referencia = referenciaGenerada, // evita NULL en la BD
                    FechaPago = null
                };

                // Usamos la sobrecarga CrearOrden(Orden) que incluimos en ClOrdenLogica
                int idOrden = logicaOrden.CrearOrden(orden);
                if (idOrden <= 0)
                {
                    lblMensaje.Text = "Error al crear la orden";
                    return;
                }

                // 5) Registrar detalles
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
                // Total en formato "universal" con punto decimal para que JS lo use directamente
                hdnTotal.Value = total.ToString("F2", CultureInfo.InvariantCulture);
                lblTotal.Text = total.ToString("C", CultureInfo.CreateSpecificCulture("es-CO")); // mostrar en COP

                // Guardar en Session el idOrden por si se necesita luego
                Session["idOrden"] = idOrden;
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
            response: 'https://lue-premoral-rosa.ngrok-free.dev/Vista/ConfirmacionEpayco.aspx',
            confirmation: 'https://lue-premoral-rosa.ngrok-free.dev/Vista/ConfirmacionEpayco.aspx'
        }});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenEpayco", script, true);
        }

        // Clase auxiliar (coincide con la que usas antes)
        public class ItemCarrito
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public double precio { get; set; }
        }
    }
}