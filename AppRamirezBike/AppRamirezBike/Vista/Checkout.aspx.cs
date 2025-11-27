using AppRamirezBike.Datos;
using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                if (Request.Cookies["email"] == null)
                {
                    Response.Redirect("Login.aspx?msg=Debe iniciar sesión");
                    return;
                }
            }
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Verificar usuario
                string correo = Request.Cookies["email"]?.Value;
                if (string.IsNullOrEmpty(correo))
                {
                    lblMensaje.Text = "Debe iniciar sesión para pagar";
                    return;
                }

                int idUsuario = logicaUsuario.ObtenerIdPorEmail(correo);
                if (idUsuario == 0)
                {
                    lblMensaje.Text = "Usuario no encontrado";
                    return;
                }

                // 2️⃣ Leer carrito
                string carritoJson = hdnCarrito.Value;
                if (string.IsNullOrEmpty(carritoJson))
                {
                    lblMensaje.Text = "El carrito está vacío";
                    return;
                }

                List<ItemCarrito> carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemCarrito>>(carritoJson);

                // 3️⃣ Crear orden
                Orden orden = new Orden
                {
                    IdUsuario = idUsuario,
                    Fecha = DateTime.Now,
                    Estado = "Pendiente",
                    Total = 0
                };

                int idOrden = logicaOrden.CrearOrden(orden);
                if (idOrden == 0)
                {
                    lblMensaje.Text = "Error al crear la orden";
                    return;
                }

                // 4️⃣ Registrar detalles y calcular total
                double totalGeneral = 0;
                foreach (var item in carrito)
                {
                    var producto = logicaProducto.ObtenerProductoPorId(item.IdProducto);
                    if (producto == null) continue;

                    double subtotal = producto.precio * item.Cantidad;
                    totalGeneral += subtotal;

                    OrdenDetalle detalle = new OrdenDetalle
                    {
                        IdOrden = idOrden,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.precio
                    };

                    logicaDetalle.RegistrarDetalle(detalle);
                }

                // 5️⃣ Actualizar total
                logicaOrden.CambiarTotal(idOrden, totalGeneral);

                // 6️⃣ Redirigir al checkout con la orden creada
                Response.Redirect($"Checkout.aspx?idOrden={idOrden}");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "⚠ Error: " + ex.Message;
            }
        }
        public class CarritoItem
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public double precio { get; set; }
        }
    }
}