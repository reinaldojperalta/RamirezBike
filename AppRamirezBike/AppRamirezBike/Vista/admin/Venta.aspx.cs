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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // --------------------------------------------------------
        // WEBMETHOD 1: ListarVentas (Para poblar la tabla principal)
        // URL de llamada: Ventas.aspx/ListarVentas
        // --------------------------------------------------------
        [WebMethod]
        public static List<Orden> ListarVentas()
        {
            // Instanciamos la Capa de Lógica
               ClVentaLogica objLogica = new ClVentaLogica();

            // Retornamos directamente el resultado del método de la capa de lógica
            return objLogica.MtListarTodasLasVentas();
        }

        // --------------------------------------------------------
        // WEBMETHOD 2: ObtenerDetalleVenta (Para expandir la fila)
        // URL de llamada: Ventas.aspx/ObtenerDetalleVenta
        // --------------------------------------------------------
        [WebMethod]
        public static List<OrdenDetalle> ObtenerDetalleVenta(int idOrden)
        {
            // Instanciamos la Capa de Lógica
            ClVentaLogica objLogica = new ClVentaLogica();

            // Retornamos el detalle de la orden solicitada
            return objLogica.MtObtenerDetallePorId(idOrden);
        }
    }
}