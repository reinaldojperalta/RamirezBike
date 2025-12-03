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
        ClVentaDatos datos = new ClVentaDatos();

        public List<VentaListado> ListarTodasLasVentas()
        {
            return datos.MtListarTodasLasVentas();
        }
    }
}