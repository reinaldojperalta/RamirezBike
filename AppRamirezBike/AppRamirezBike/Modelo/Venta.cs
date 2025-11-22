using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class Venta
    {
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaVenta { get; set; }
        public decimal totalVenta { get; set; }
        public string estado { get; set; } 

        public List<DetalleVenta> Detalles { get; set; }
    }
}
    