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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (producto == null) 
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                ClProductoLogica logica = new ClProductoLogica();
                producto = logica.ObtenerProductoPorId(id);
            }
        }
    }
}