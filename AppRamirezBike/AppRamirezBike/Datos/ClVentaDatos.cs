using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq; 

namespace AppRamirezBike.Datos
{
    public class ClVentaDatos
    {
        private ClConexion objConexion = new ClConexion();

        public int MtRegistrarVentaTransaccional(Venta objVenta, List<DetalleVenta> detallesCarrito)
        {
            decimal totalCalculado = detallesCarrito.Sum(d => d.subtotal) * (1 + 0.19M);

            SqlConnection conexion = null;
            SqlTransaction transaccion = null;
            int idVentaGenerada = 0;
            bool transaccionExitosa = false;

            try
            {
                
                conexion = objConexion.MtAbrirConexion();
                transaccion = conexion.BeginTransaction();

              
                string sqlVenta = @"
                    INSERT INTO Venta (IdCliente, IdUsuario, TotalVenta, Estado, FechaVenta) 
                    OUTPUT INSERTED.IdVenta
                    VALUES (@IdCliente, @IdUsuario, @TotalVenta, 'Completada', GETDATE());";

                using (SqlCommand cmdVenta = new SqlCommand(sqlVenta, conexion, transaccion))
                {
                    cmdVenta.Parameters.AddWithValue("@IdCliente", objVenta.idCliente);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", objVenta.idUsuario);
                    cmdVenta.Parameters.AddWithValue("@TotalVenta", totalCalculado);

                    idVentaGenerada = Convert.ToInt32(cmdVenta.ExecuteScalar());
                }

                if (idVentaGenerada > 0)
                {
                    foreach (DetalleVenta detalle in detallesCarrito)
                    {
                        string sqlDetalle = @"
                            INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) 
                            VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)";

                        using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, conexion, transaccion))
                        {
                            cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerada);
                            cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.idProducto);
                            cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.cantidad);
                            cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.precioUnitario);
                            cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.subtotal);
                            cmdDetalle.ExecuteNonQuery();
                        }

                        string sqlStock = "UPDATE Producto SET stock = stock - @Cantidad WHERE IdProducto = @IdProducto";
                        using (SqlCommand cmdStock = new SqlCommand(sqlStock, conexion, transaccion))
                        {
                            cmdStock.Parameters.AddWithValue("@Cantidad", detalle.cantidad);
                            cmdStock.Parameters.AddWithValue("@IdProducto", detalle.idProducto);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    transaccionExitosa = true; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la transacción de venta: " + ex.Message);
            }
            finally
            {
                if (transaccion != null)
                {
                    if (transaccionExitosa)
                    {
                        transaccion.Commit(); // *** CONFIRMAR (Guardar) ***
                    }
                    else
                    {
                        transaccion.Rollback(); // *** DESHACER (Cancelar) ***
                        idVentaGenerada = 0;
                    }
                }

                // CERRAR CONEXIÓN
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    objConexion.MtCerrarConexion();
                }
            }

            return idVentaGenerada;
        }
    }
}