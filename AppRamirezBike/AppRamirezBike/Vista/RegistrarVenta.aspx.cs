using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vistas
{
    public partial class RegistrarVenta : System.Web.UI.Page
    {
        // Instancias de Capa de Lógica
        private ClVentaLogica objVentaLogica = new ClVentaLogica();
        private ClProductoLogica objProductoLogica = new ClProductoLogica();

        // Constantes para IDs de ejemplo (deben ser reemplazadas por la lógica de sesión real)
        private const int ID_CLIENTE_DEMO = 1; // ID para 'Cliente Público' si no se selecciona otro
        private const int ID_USUARIO_DEMO = 5; // ID del empleado o usuario logueado

        
        private List<DetalleVenta> Carrito
        {
            get
            {
                if (Session["CarritoVenta"] == null)
                    Session["CarritoVenta"] = new List<DetalleVenta>();
                return (List<DetalleVenta>)Session["CarritoVenta"];
            }
            set { Session["CarritoVenta"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarProductos(0, 0, "");
            }

            MtCargarDetallesCarrito();
            MtCalcularTotales();
        }

       
        protected void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscarProducto.Text.Trim();
            int idProductoBuscado = 0;

            if (int.TryParse(textoBusqueda, out idProductoBuscado) && idProductoBuscado > 0)
            {
                MtCargarProductoPorId(idProductoBuscado);
            }
            else
            {
                MtCargarProductos(0, 0, textoBusqueda);
            }
        }

        private void MtCargarProductos(int idCategoria, int numeroPagina, string textoBusqueda)
        {
            int tamañoPagina = 10;
            int salto = numeroPagina * tamañoPagina;
            int totalRegistros;

            List<Producto> listaProductos = objProductoLogica.MtDatosVistaProducto(
                salto,
                tamañoPagina,
                out totalRegistros,
                idCategoria,
                textoBusqueda
            );

            gridProductos.DataSource = listaProductos;
            gridProductos.DataBind();
            gridProductos.Visible = true;
        }

        private void MtCargarProductoPorId(int idProducto)
        {
            Producto productoEncontrado = objProductoLogica.ObtenerProductoPorId(idProducto);
            List<Producto> listaResultado = new List<Producto>();

            if (productoEncontrado != null)
            {
                listaResultado.Add(productoEncontrado);
            }

            gridProductos.DataSource = listaResultado;
            gridProductos.DataBind();
            gridProductos.Visible = true;

            if (productoEncontrado == null)
            {
                MostrarAlerta("Búsqueda", $"No se encontró ningún producto con el ID exacto: {idProducto}.", "info");
            }
        }

        protected void gridProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Añadir")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                Producto objProducto = objProductoLogica.ObtenerProductoPorId(idProducto);

                if (objProducto != null && objProducto.stock > 0 && objProducto.precio > 0)
                {
                    DetalleVenta detalleExistente = Carrito.FirstOrDefault(d => d.idProducto == idProducto);

                    if (detalleExistente != null)
                    {
                        // Incrementa la cantidad con validación de stock
                        if (detalleExistente.cantidad + 1 <= objProducto.stock)
                        {
                            detalleExistente.cantidad++;
                            detalleExistente.subtotal = detalleExistente.cantidad * detalleExistente.precioUnitario;
                        }
                        else
                        {
                            MostrarAlerta("Stock Insuficiente", $"No hay más de {objProducto.stock} unidades de este producto en stock.", "warning");
                            return;
                        }
                    }
                    else
                    {
                        // Nuevo detalle
                        DetalleVenta nuevoDetalle = new DetalleVenta()
                        {
                            idProducto = objProducto.idProducto,
                            nombreProducto = objProducto.nombre,
                            precioUnitario = objProducto.precio,
                            cantidad = 1,
                            subtotal = objProducto.precio * 1
                        };
                        Carrito.Add(nuevoDetalle);
                    }

                    MtCargarDetallesCarrito();
                    MtCalcularTotales();
                }
                else
                {
                    MostrarAlerta("Error", "Producto no encontrado, sin stock o precio no definido (0).", "error");
                }
            }
        }

        protected void gridCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                if (int.TryParse(e.CommandArgument.ToString(), out int idProductoAQuitar))
                {
                    List<DetalleVenta> carritoActual = this.Carrito;
                    DetalleVenta productoARemover = carritoActual.FirstOrDefault(d => d.idProducto == idProductoAQuitar);

                    if (productoARemover != null)
                    {
                        if (productoARemover.cantidad > 1)
                        {
                            // Disminuye la cantidad
                            productoARemover.cantidad--;
                            productoARemover.subtotal = productoARemover.cantidad * productoARemover.precioUnitario;
                            MostrarAlerta("Carrito", $"Se redujo la cantidad del producto ID {idProductoAQuitar} a {productoARemover.cantidad}.", "info");
                        }
                        else
                        {
                            // Remueve el item
                            carritoActual.Remove(productoARemover);
                            MostrarAlerta("Carrito", $"Se ha eliminado el producto ID {idProductoAQuitar} del carrito.", "info");
                        }

                        this.Carrito = carritoActual;
                        MtCargarDetallesCarrito();
                        MtCalcularTotales();
                    }
                }
            }
        }

        private void MtCargarDetallesCarrito()
        {
            gridCarrito.DataSource = Carrito;
            gridCarrito.DataBind();
        }

        private void MtCalcularTotales()
        {
            decimal subTotalCarrito = Carrito.Sum(d => d.subtotal);
            decimal impuestoPorcentaje = 0.19M;
            decimal impuestos = subTotalCarrito * impuestoPorcentaje;
            decimal totalAPagar = subTotalCarrito + impuestos;

            lblSubtotal.Text = subTotalCarrito.ToString("C");
            lblImpuesto.Text = impuestos.ToString("C");
            lblTotalVenta.Text = totalAPagar.ToString("C");
        }


        protected void BtnConfirmarPago_Click(object sender, EventArgs e)
        {
            List<DetalleVenta> detallesCarrito = Carrito;

            if (detallesCarrito == null || detallesCarrito.Count == 0)
            {
                MostrarAlerta("Error", "Debe agregar al menos un producto al carrito para registrar la venta.", "warning");
                return;
            }

            int idCliente = ID_CLIENTE_DEMO;
            if (ddlCliente != null && int.TryParse(ddlCliente.SelectedValue, out int clienteId))
            {
                idCliente = clienteId;
            }

            string mensajeError = string.Empty;

            int idVentaGenerada = objVentaLogica.MtProcesarVenta(
                idCliente,
                ID_USUARIO_DEMO,
                detallesCarrito,
                out mensajeError
            );

            if (idVentaGenerada > 0)
            {
                MostrarAlerta("¡Venta Exitosa!", $"Venta No. {idVentaGenerada} registrada. Stock actualizado.", "success");

                MtLimpiarInterfazVenta();
                MtCargarProductos(0, 0, "");
            }
            else
            {
                
                string mensajeFinal = string.IsNullOrEmpty(mensajeError)
                    ? "Error desconocido: la venta no se registró. Por favor, verifique el log de la aplicación."
                    : mensajeError;

                MostrarAlerta("Error de Venta", mensajeFinal, "error");
            }
        }

        private void MtLimpiarInterfazVenta()
        {
            Session["CarritoVenta"] = null;
            txtBuscarProducto.Text = string.Empty;
            MtCargarDetallesCarrito();
            MtCalcularTotales();
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            string emoji = "";
            switch (tipo.ToLower())
            {
                case "success": emoji = "✅"; break;
                case "error": emoji = "❌"; break;
                case "warning": emoji = "⚠️"; break;
                case "info": emoji = "ℹ️"; break;
                default: emoji = "📢"; break;
            }

            string script = $"alert('{emoji} {titulo}\\n\\n{mensaje}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "AlertScript", script, true);
        }
    }
}