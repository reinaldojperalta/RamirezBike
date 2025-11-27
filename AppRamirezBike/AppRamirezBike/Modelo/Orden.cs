using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }  // Pendiente, Pagado, Fallido
        public DateTime Fecha { get; set; }
        public string RefPago { get; set; } // Código o referencia de ePayco

    }
}