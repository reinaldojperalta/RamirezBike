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
            if (producto == null && !IsPostBack) // Añadir !IsPostBack para asegurar que solo cargue una vez
            {
                // 1. Obtener ID y cargar UNA SOLA VEZ
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    producto = logica.ObtenerProductoPorId(id);

                    if (producto != null)
                        MtCargarDetalle();
                }
            }
        }
        private void MtCargarDetalle()
        {
            // 2. Usar el objeto 'producto' que ya se cargó en Page_Load

            /* imagen */
            imgPrincipal.ImageUrl = "img/" + producto.imgUrl;
            imgPrincipal.AlternateText = producto.nombre;

            /* textos */
            lblNombre.Text = producto.nombre;
            lblDescripcion.Text = producto.descripcion;
            lblSKU.Text = "SKU: " + producto.idProducto;

            /* precio */
            lblPrecio.Text = "$" + producto.precio.ToString("0.00");
            lblPrecioOriginal.Text = string.Empty;

            /* cantidad por defecto */
            txtCantidad.Text = "1";
        }
    }
}