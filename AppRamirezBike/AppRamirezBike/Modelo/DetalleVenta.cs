using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class DetalleVenta
    {
        public int idDetalleVenta { get; set; }
        public int idVenta { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }

        // Propiedad para cargar la información del producto (no se persiste directamente)
        public Producto ProductoVendido { get; set; }   
    }
}