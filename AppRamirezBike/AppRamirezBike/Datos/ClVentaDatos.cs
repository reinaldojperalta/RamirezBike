using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClVentaDatos
    {
        public List<Orden> MtListarTodasLasVentas()
        {
            ClConexion objConexion = new ClConexion();
            List<Orden> listaOrdenes = new List<Orden>();
            SqlConnection conex = null;

            conex = objConexion.MtAbrirConexion();

            string consulta = @"
        SELECT IdOrden, IdUsuario, Referencia, Total, Estado, MetodoPago, FechaCreacion, FechaPago
        FROM Orden 
        ORDER BY FechaCreacion DESC";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Nullable<DateTime> fechaPago;

                if (reader["FechaPago"] != DBNull.Value)
                {
                    // Usamos Convert.ToDateTime() para asegurar que la conversión sea correcta
                    fechaPago = Convert.ToDateTime(reader["FechaPago"]);
                }
                else
                {
                    fechaPago = null; // Simplificamos (Nullable<DateTime>)null a null
                }

                listaOrdenes.Add(new Orden()
                {
                    IdOrden = Convert.ToInt32(reader["IdOrden"]),
                    IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                    Referencia = reader["Referencia"].ToString(),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Estado = reader["Estado"].ToString(),
                    MetodoPago = reader["MetodoPago"].ToString(),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                    FechaPago = fechaPago
                });
            }
            reader.Close();
            objConexion.MtCerrarConexion();

            return listaOrdenes;
        }


        public List<OrdenDetalle> MtObtenerDetallePorId(int idOrden)
        {
            ClConexion objConexion = new ClConexion();
            List<OrdenDetalle> listaDetalles = new List<OrdenDetalle>();
            SqlConnection conex = null;

            conex = objConexion.MtAbrirConexion();

            // Consulta que TRAE SOLO LAS COLUMNAS DEFINIDAS EN TU MODELO OrdenDetalle
            string consulta = @"
                SELECT 
                    IdDetalle, 
                    IdOrden, 
                    IdProducto, 
                    Cantidad, 
                    PrecioUnitario, 
                    Subtotal
                FROM OrdenDetalle 
                WHERE IdOrden = @idOrden";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            cmd.Parameters.AddWithValue("@idOrden", idOrden);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                listaDetalles.Add(new OrdenDetalle()
                {
                    IdOrdenDetalle = Convert.ToInt32(reader["IdDetalle"]),
                    IdOrden = Convert.ToInt32(reader["IdOrden"]),
                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                    Subtotal = Convert.ToDecimal(reader["Subtotal"])
                });
            }
            reader.Close();
            objConexion.MtCerrarConexion();

            return listaDetalles;
        }
    }
}