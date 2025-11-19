namespace AppRamirezBike.Modelo
{
    public class DetallePedido
    {
        public int idDetallePedido { get; set; }

        public int idPedido { get; set; }

        public int idProducto { get; set; }

        public int cantidad { get; set; }

        public decimal precioVenta { get; set; }

        public string nombre { get; set; }
    }
}