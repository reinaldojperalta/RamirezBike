using AppRamirezBike.Modelo;
using AppRamirezBike.Datos;
using System.Collections.Generic;
using System.Linq; // Necesario para la función .Sum()
using System.Web; // Necesario para acceder a la sesión (HttpContext)

namespace AppRamirezBike.Logica
{
    public class ClPedidoLogica
    {
        private readonly ClPedidoDatos objPedidoDatos = new ClPedidoDatos();

        /// <summary>
        /// Procesa la venta: valida datos, calcula total, obtiene el ID de usuario y llama a la transacción.
        /// </summary>
        public bool MtProcesarVenta(List<DetallePedido> detallesVentaCliente)
        {
            // LÓGICA 1: Validación de la lista de productos
            if (detallesVentaCliente == null || detallesVentaCliente.Count == 0)
            {
                return false;
            }

            // LÓGICA 2: Obtener el ID del Empleado (Usuario) de la Sesión
            // CRÍTICO: Si no hay usuario en sesión, la venta no puede registrarse.
            if (HttpContext.Current == null || HttpContext.Current.Session == null || HttpContext.Current.Session["idUsuario"] == null)
            {
                return false;
            }

            // Asumimos que el ID se guardó como int en la sesión
            int idEmpleado = (int)HttpContext.Current.Session["idUsuario"];


            // LÓGICA 3: Calcular el Total de la Venta
            // Multiplicamos cantidad * precioVenta para obtener el total de todos los ítems.
            decimal totalVenta = detallesVentaCliente.Sum(d => d.cantidad * d.precioVenta);

            Pedido nuevoPedido = new Pedido
            {
                idUsuario = idEmpleado,
                fecha = System.DateTime.Now,
                total = totalVenta
            };

            // LÓGICA 4: Llamar al método transaccional (Capa de Datos)
            bool resultadoTransaccion = objPedidoDatos.MtGuardarVentaConTransaccion(nuevoPedido, detallesVentaCliente);

            return resultadoTransaccion;
        }
    }
}