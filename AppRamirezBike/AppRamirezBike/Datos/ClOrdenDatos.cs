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

        public int RegistrarOrden(Orden orden)
        {
            string query = "INSERT INTO Orden(idUsuario,fecha,total,estado,referenciaPago) OUTPUT INSERTED.idOrden " +
                           "VALUES(@idUsuario,@fecha,@total,@estado,@referenciaPago)";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idUsuario", orden.IdUsuario);
            cmd.Parameters.AddWithValue("@fecha", orden.Fecha);
            cmd.Parameters.AddWithValue("@total", orden.Total);
            cmd.Parameters.AddWithValue("@estado", orden.Estado);
            cmd.Parameters.AddWithValue("@referenciaPago", orden.RefPago ?? (object)DBNull.Value);

            int idOrden = (int)cmd.ExecuteScalar();
            conexion.MtCerrarConexion();
            return idOrden;
        }

        public int ActualizarEstado(int idOrden, string estado)
        {
            string query = "UPDATE Orden SET estado=@estado WHERE idOrden=@idOrden";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@estado", estado);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }
        public int RegistrarDetalle(OrdenDetalle detalle)
        {
            ClConexion oConexion = new ClConexion();

            string consulta = @"INSERT INTO OrdenDetalle (idOrden, idProducto, cantidad, precioUnitario)
                        VALUES (@idOrden, @idProducto, @cantidad, @precioUnitario)";

            SqlCommand cmd = new SqlCommand(consulta, conexion.MtAbrirConexion());

            cmd.Parameters.AddWithValue("@idOrden", detalle.IdOrden);
            cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);

            int result = cmd.ExecuteNonQuery();

            oConexion.MtCerrarConexion();

            return result;
        }
        public int ActualizarTotal(int idOrden, double total)
        {
            string consulta = "UPDATE Orden SET total=@total WHERE idOrden=@idOrden";

            SqlCommand cmd = new SqlCommand(consulta, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }

    }
}