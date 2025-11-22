using AppRamirezBike.Logica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarritoDesdeURL();
            }
        }
        private void CargarCarritoDesdeURL()
        {
            string datos = Request.QueryString["datos"];

            if (string.IsNullOrEmpty(datos))
            {
                pnlVacio.Visible = true;
                pnlCarrito.Visible = false;
                return;
            }

            var logica = new ClProductoLogica();
            var listaCarrito = new List<ItemCarrito>();
            double totalGeneral = 0;

            string[] pares = datos.Split(',');
            foreach (string par in pares)
            {
                string[] partes = par.Split('-');
                if (partes.Length != 2) continue;

                int id = int.Parse(partes[0]);
                int cantidad = int.Parse(partes[1]);

                var producto = logica.ObtenerProductoPorId(id);
                if (producto != null)
                {
                    double subtotal = producto.precioProovedor * cantidad;
                    totalGeneral += subtotal;

                    listaCarrito.Add(new ItemCarrito
                    {
                        IdProducto = producto.idProducto,
                        Nombre = producto.nombre,
                        Precio = producto.precioProovedor,
                        Cantidad = cantidad,
                        ImgUrl = producto.imgUrl,
                        Subtotal = subtotal
                    });
                }
            }

            if (listaCarrito.Count == 0)
            {
                pnlVacio.Visible = true;
                pnlCarrito.Visible = false;
            }
            else
            {
                pnlVacio.Visible = false;
                pnlCarrito.Visible = true;
                rptCarrito.DataSource = listaCarrito;
                rptCarrito.DataBind();
                lblTotal.Text = totalGeneral.ToString("N0");
            }
        }
    }

    // Clase auxiliar para el Repeater
    public class ItemCarrito
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string ImgUrl { get; set; }
        public double Subtotal { get; set; }
    }

}
