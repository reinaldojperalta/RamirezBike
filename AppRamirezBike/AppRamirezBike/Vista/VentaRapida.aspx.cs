using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;

namespace AppRamirezBike.Vista
{
    public partial class VentaRapida : System.Web.UI.Page
    {
       
        [WebMethod]
        public static string FinalizarVenta(List<DetallePedido> detallesVenta)
        {
            ClPedidoLogica objLogica = new ClPedidoLogica();

            bool exito = objLogica.MtProcesarVenta(detallesVenta);

            // Devolver un mensaje informativo al JavaScript (T4)
            if (exito)
            {
                // La venta y la reducción de stock fueron exitosas (COMMIT)
                return "Venta registrada con éxito y stock actualizado.";
            }
            else
            {
                // Fallo lógico: stock insuficiente, lista vacía, o idUsuario no encontrado en sesión (ROLLBACK)
                return "Error: No se pudo finalizar la venta. Verifique la autenticación del empleado o si el stock es suficiente.";
            }
        }
    }
}