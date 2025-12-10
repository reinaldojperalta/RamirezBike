using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista.admin
{
    public partial class Venta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        [WebMethod]
        public static List<VentaCabecera> ListarTodasLasVentas()
        {
            ClVentaLogica logica = new ClVentaLogica();
            return logica.ListarVentasConDetalle();
        }

    }
}