namespace AppRamirezBike.Modelo
{
    public class Pedido
    {
        public int idPedido { get; set; }

        public int idUsuario { get; set; }

        public System.DateTime fecha { get; set; }

        public decimal total { get; set; }

        public string estado { get; set; }
    }
}