using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClOrdenLogica
    {

        ClOrdenDatos datos = new ClOrdenDatos();

        public int CrearOrden(Orden orden)
        {
            return datos.RegistrarOrden(orden);
        }

        public int CambiarEstado(int idOrden, string estado)
        {
            return datos.ActualizarEstado(idOrden, estado);
        }
        public int CrearOrdenSimple(int idUsuario, decimal total)
        {
            Orden orden = new Orden
            {
                IdUsuario = idUsuario,
                Total = total,
                Estado = "Pendiente",
                Fecha = DateTime.Now
            };

            return datos.RegistrarOrden(orden);
        }
        public int CrearOrdenConDetalles(Orden orden, List<OrdenDetalle> detalles)
        {
            int idOrden = datos.RegistrarOrden(orden);

            if (idOrden > 0)
            {
                foreach (var d in detalles)
                {
                    d.IdOrden = idOrden;
                    datos.RegistrarDetalle(d);
                }
            }

            return idOrden;
        }
        public int CambiarTotal(int idOrden, double total)
        {
            return datos.ActualizarTotal(idOrden, total);
        }

    }
}