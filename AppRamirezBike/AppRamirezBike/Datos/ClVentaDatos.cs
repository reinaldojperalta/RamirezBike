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
        public List<VentaCabecera> MtListarVentasConDetalle()
        {
            ClConexion objConexion = new ClConexion();
            List<VentaCabecera> lista = new List<VentaCabecera>();
            SqlConnection conex = null;
            conex = objConexion.MtAbrirConexion();

            string consulta = @"
                SELECT 
                    o.IdOrden, o.Referencia, o.FechaCreacion, o.Total, o.Estado, o.MetodoPago,
                    CONCAT(u.nombre, ' ', u.apellido) AS Cliente,
                    p.nombre AS Producto, od.Cantidad, od.PrecioUnitario, od.Subtotal
                FROM Orden o
                INNER JOIN usuario u ON o.IdUsuario = u.idUsuario
                INNER JOIN OrdenDetalle od ON o.IdOrden = od.IdOrden
                INNER JOIN producto p ON od.IdProducto = p.idProducto
                ORDER BY o.FechaCreacion DESC, o.IdOrden DESC";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            SqlDataReader reader = cmd.ExecuteReader();

            Dictionary<int, VentaCabecera> diccionario = new Dictionary<int, VentaCabecera>();

            while (reader.Read())
            {
                int idOrden = Convert.ToInt32(reader["IdOrden"]);

                if (!diccionario.ContainsKey(idOrden))
                {
                    VentaCabecera cabecera = new VentaCabecera();
                    cabecera.IdOrden = idOrden;
                    cabecera.Referencia = reader["Referencia"].ToString();
                    cabecera.Cliente = reader["Cliente"].ToString();
                    cabecera.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                    cabecera.Total = Convert.ToDecimal(reader["Total"]);
                    cabecera.Estado = reader["Estado"].ToString();
                    cabecera.MetodoPago = reader["MetodoPago"] == DBNull.Value ? "N/A" : reader["MetodoPago"].ToString();

                    diccionario.Add(idOrden, cabecera);
                }

                DetalleProducto detalle = new DetalleProducto();
                detalle.Producto = reader["Producto"].ToString();
                detalle.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                detalle.PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]);
                detalle.Subtotal = Convert.ToDecimal(reader["Subtotal"]);

                diccionario[idOrden].Detalles.Add(detalle);
            }

            reader.Close();

            for (int i = 0; i < diccionario.Count; i++)
            {
                lista.Add(diccionario.ElementAt(i).Value);
            }

            objConexion.MtCerrarConexion();
            return lista;
        }
    }
}
