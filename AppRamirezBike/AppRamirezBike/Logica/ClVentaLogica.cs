using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClVentaLogica
    {
        ClVentaDatos objDatos = new ClVentaDatos();

        // --------------------------------------------------------
        // MÉTODO 1: Listar todas las órdenes (Cabecera)
        // --------------------------------------------------------
        public List<Orden> MtListarTodasLasVentas()
        {
            // Aquí iría cualquier regla de negocio antes de obtener la lista
            // Por ahora, solo delegamos la llamada a la capa de datos.
            return objDatos.MtListarTodasLasVentas();
        }

        // --------------------------------------------------------
        // MÉTODO 2: Obtener el detalle de una orden específica
        // --------------------------------------------------------
        public List<OrdenDetalle> MtObtenerDetallePorId(int idOrden)
        {
            // Aquí iría la lógica para validar el idOrden si fuera necesario
            return objDatos.MtObtenerDetallePorId(idOrden);
        }
    }
}