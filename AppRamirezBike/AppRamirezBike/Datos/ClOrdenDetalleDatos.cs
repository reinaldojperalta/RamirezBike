using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClOrdenDetalleDatos
    {
        ClConexion conexion = new ClConexion();

        public int RegistrarDetalle(OrdenDetalle detalle)
        {
            string query = "INSERT INTO OrdenDetalle(idOrden,idProducto,cantidad,precioUnitario,subtotal) " +
                           "VALUES(@idOrden,@idProducto,@cantidad,@precioUnitario,@subtotal)";

            SqlCommand cmd = new SqlCommand(query, conexion.MtAbrirConexion());
            cmd.Parameters.AddWithValue("@idOrden", detalle.IdOrden);
            cmd.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
            cmd.Parameters.AddWithValue("@subtotal", detalle.Subtotal);

            int filas = cmd.ExecuteNonQuery();
            conexion.MtCerrarConexion();
            return filas;
        }
    }
}