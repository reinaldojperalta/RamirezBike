using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
    public partial class Detalle : System.Web.UI.Page
    {
        public Producto producto= null;
        ClProductoLogica logica = new ClProductoLogica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (producto == null) 
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                producto = logica.ObtenerProductoPorId(id);

                if (producto != null)
                    MtCargarDetalle();
            }
        }
        private void MtCargarDetalle()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            producto = logica.ObtenerProductoPorId(id);

            /* imagen */
            imgPrincipal.ImageUrl = "img/" +  producto.imgUrl;   // imgUrl (minúscula)
            imgPrincipal.AlternateText = producto.nombre;

            /* textos */
            lblNombre.Text = producto.nombre;
            lblDescripcion.Text = producto.descripcion;
            lblSKU.Text = "SKU: " + producto.idProducto;  // o el campo que uses como SKU

            /* precio */
            lblPrecio.Text = "$" + producto.precio.ToString("0.00");

            /* si más adelante tienes “precioOriginal” agrégalo; por ahora solo uno */
            lblPrecioOriginal.Text = string.Empty;

            /* cantidad por defecto */
            txtCantidad.Text = "1";

            /* id para el JS */
        }
    }
}