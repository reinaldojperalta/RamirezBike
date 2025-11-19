using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppRamirezBike.Modelo;

namespace AppRamirezBike.Datos
{
    public class ClPedidoDatos
    {
        private readonly ClConexion objConexion = new ClConexion();

        public bool MtGuardarVentaConTransaccion(Pedido pedido, List<DetallePedido> detalles)
        {
            using (SqlConnection conexion = objConexion.MtAbrirConexion())
            {
                conexion.Open();

                SqlTransaction transaccion = conexion.BeginTransaction();
                int idPedidoGenerado = 0;

                string sqlInsertPedido = "INSERT INTO Pedido (idUsuario, fechaPedido, total, estado) VALUES (@idUsuario, GETDATE(), @total, 'Finalizado'); SELECT SCOPE_IDENTITY();";
                using (SqlCommand comandoPedido = new SqlCommand(sqlInsertPedido, conexion, transaccion))
                {
                    comandoPedido.Parameters.AddWithValue("@idUsuario", pedido.idUsuario);
                    comandoPedido.Parameters.AddWithValue("@total", pedido.total);

                    object resultado = comandoPedido.ExecuteScalar();
                    if (resultado != null)
                    {
                        idPedidoGenerado = Convert.ToInt32(resultado);
                    }
                }

                if (idPedidoGenerado <= 0)
                {
                    transaccion.Rollback();
                    return false;
                }

                foreach (var detalle in detalles)
                {
                    string sqlInsertDetalle = "INSERT INTO DetallePedido (idPedido, idProducto, cantidad, precioVenta) VALUES (@idPedido, @idProducto, @cantidad, @precioVenta)";
                    using (SqlCommand comandoDetalle = new SqlCommand(sqlInsertDetalle, conexion, transaccion))
                    {
                        comandoDetalle.Parameters.AddWithValue("@idPedido", idPedidoGenerado);
                        comandoDetalle.Parameters.AddWithValue("@idProducto", detalle.idProducto);
                        comandoDetalle.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        comandoDetalle.Parameters.AddWithValue("@precioVenta", detalle.precioVenta);

                        if (comandoDetalle.ExecuteNonQuery() <= 0)
                        {
                            transaccion.Rollback();
                            return false;
                        }
                    }

                    
                    string sqlUpdateStock = "UPDATE Producto SET stock = stock - @cantidad WHERE idProducto = @idProducto AND stock >= @cantidad";
                    using (SqlCommand comandoStock = new SqlCommand(sqlUpdateStock, conexion, transaccion))
                    {
                        comandoStock.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        comandoStock.Parameters.AddWithValue("@idProducto", detalle.idProducto);

                        if (comandoStock.ExecuteNonQuery() <= 0)
                        {
                            transaccion.Rollback();
                            return false;
                        }
                    }
                }

                
                // Si la ejecución llega a este punto, todas las comprobaciones lógicas pasaron.
                transaccion.Commit();
                return true;
            } // El 'using' asegura que la conexión se cierre.
        }
    }
}