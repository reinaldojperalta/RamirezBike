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
        public List<VentaCabecera> ListarVentasConDetalle()
        {
            ClVentaDatos datos = new ClVentaDatos();
            return datos.MtListarVentasConDetalle();
        }
    }
}