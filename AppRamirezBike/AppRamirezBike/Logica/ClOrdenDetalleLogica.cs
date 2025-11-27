using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClOrdenDetalleLogica
    {
        ClOrdenDetalleDatos datos = new ClOrdenDetalleDatos();

        public int GuardarDetalle(OrdenDetalle detalle)
        {
            return datos.RegistrarDetalle(detalle);
        }
    public int RegistrarDetalle(OrdenDetalle detalle)
        {
            ClOrdenDetalleDatos datos = new ClOrdenDetalleDatos();
            return datos.RegistrarDetalle(detalle);
        }

    }
}