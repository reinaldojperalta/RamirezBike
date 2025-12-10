using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class VentaCabecera
    {
        public int IdOrden { get; set; }
        public string Referencia { get; set; }
        public string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public List<DetalleProducto> Detalles { get; set; } = new List<DetalleProducto>();
    }

    public class DetalleProducto
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}