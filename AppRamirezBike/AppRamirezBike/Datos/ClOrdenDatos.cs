using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClOrdenDatos
    {
        ClConexion conexion = new ClConexion();

        // Registrar orden (antes del pago)
        public int RegistrarOrden(Orden orden)
        {
            string query = @"INSERT INTO Orden (idUsuario, Referencia, Total, Estado, MetodoPago, FechaCreacion, FechaPago)
                         OUTPUT INSERTED.idOrden
                         VALUES (@idUsuario, @Referencia, @Total, @Estado, @MetodoPago, @FechaCreacion, @FechaPago)";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());

            cmd.Parameters.AddWithValue("@idUsuario", orden.IdUsuario);
            cmd.Parameters.AddWithValue("@Referencia", orden.Referencia ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Total", orden.Total);
            cmd.Parameters.AddWithValue("@Estado", orden.Estado);
            cmd.Parameters.AddWithValue("@MetodoPago", orden.MetodoPago ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaCreacion", orden.FechaCreacion);
            cmd.Parameters.AddWithValue("@FechaPago", orden.FechaPago ?? (object)DBNull.Value);

            int idOrden = (int)cmd.ExecuteScalar();
            conexion.MtCerrarConexion();
            return idOrden;
        }

        // Actualizar estado (respuesta de ePayco)
        public int ActualizarEstado(int idOrden, string estado)
        {
            string query = "UPDATE Orden SET Estado=@Estado WHERE idOrden=@idOrden";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@Estado", estado);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }

        // Registrar detalle
        public int RegistrarDetalle(OrdenDetalle detalle)
        {
            string consulta = @"INSERT INTO OrdenDetalle (idOrden, idProducto, cantidad, precioUnitario)
                            VALUES (@idOrden, @idProducto, @cantidad, @precioUnitario)";

            SqlCommand cmd = new SqlCommand(consulta, conexion.MtAbrirConexion());

            cmd.Parameters.AddWithValue("@idOrden", detalle.IdOrden);
            cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);

            int result = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return result;
        }

        // Actualizar total (si cambian productos)
        public int ActualizarTotal(int idOrden, decimal total)
        {
            string consulta = "UPDATE Orden SET Total=@Total WHERE idOrden=@idOrden";

            SqlCommand cmd = new SqlCommand(consulta, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@Total", total);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }

        // Guardar referencia generada por ePayco
        public int ActualizarReferencia(int idOrden, string referencia)
        {
            string query = "UPDATE Orden SET Referencia=@Referencia WHERE idOrden=@idOrden";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@Referencia", referencia);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }

       
        public int ActualizarEstadoPorReferencia(string referencia, string nuevoEstado)
        {
            string query = @"UPDATE Orden 
                     SET Estado = @Estado, FechaPago = GETDATE()
                     WHERE Referencia = @Referencia";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
            cmd.Parameters.AddWithValue("@Referencia", referencia);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }
        public int ActualizarFechaPagoPorReferencia(string referencia, DateTime fechaPago)
        {
            string query = "UPDATE Orden SET FechaPago=@FechaPago WHERE Referencia=@Referencia";
            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@FechaPago", fechaPago);
            cmd.Parameters.AddWithValue("@Referencia", referencia);
            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }
    }
}