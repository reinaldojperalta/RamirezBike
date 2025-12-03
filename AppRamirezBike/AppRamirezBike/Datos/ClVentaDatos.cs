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
        public List<VentaListado> MtListarTodasLasVentas()
        {
            ClConexion objConexion = new ClConexion();
            List<VentaListado> lista = new List<VentaListado>();
            SqlConnection conex = objConexion.MtAbrirConexion();

            string consulta = @"
                SELECT 
                    o.IdOrden,
                    o.Referencia,
                    CONCAT(u.nombre, ' ', u.apellido) AS Cliente,
                    u.email AS Email,
                    o.FechaCreacion,
                    o.Total,
                    o.Estado,
                    o.MetodoPago,
                    p.nombre AS Producto,
                    od.Cantidad,
                    od.PrecioUnitario,
                    (od.Cantidad * od.PrecioUnitario) AS Subtotal
                FROM Orden o
                INNER JOIN usuario u ON o.IdUsuario = u.idUsuario
                INNER JOIN OrdenDetalle od ON o.IdOrden = od.IdOrden
                INNER JOIN producto p ON od.IdProducto = p.idProducto
                ORDER BY o.FechaCreacion DESC";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new VentaListado()
                {
                    IdOrden = Convert.ToInt32(reader["IdOrden"]),
                    Referencia = reader["Referencia"].ToString(),
                    Cliente = reader["Cliente"].ToString(),
                    Email = reader["Email"].ToString(),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Estado = reader["Estado"].ToString(),
                    MetodoPago = reader["MetodoPago"] == DBNull.Value ? "N/A" : reader["MetodoPago"].ToString(),
                    Producto = reader["Producto"].ToString(),
                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                    Subtotal = Convert.ToDecimal(reader["Subtotal"])
                });
            }

            reader.Close();
            objConexion.MtCerrarConexion();
            return lista;
        }
    }
}
