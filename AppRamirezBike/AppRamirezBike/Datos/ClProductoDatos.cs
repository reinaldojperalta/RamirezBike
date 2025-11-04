using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClProductoDatos
    {
        public List<Producto> MtListarProductos()
        {
            List<Producto> objProductosList=new List<Producto>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            string consulta = "SELECT IdProducto, nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha FROM dbo.producto ";
            conex = objConexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand(consulta,conex);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto objProducto = new Producto 
                {
                    idProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                    nombre = reader["nombre"].ToString(),
                    descripcion = reader["descripcion"].ToString(),
                    precio = reader.GetInt32(reader.GetOrdinal("precio")),
                    imgUrl = reader["imgUrl"].ToString(),
                    stock = reader.GetInt32(reader.GetOrdinal("stock")),
                    estado = reader.GetBoolean(reader.GetOrdinal("estado")),
                    CreacionFecha = reader.GetDateTime(reader.GetOrdinal("CreacionFecha")),
                    ModificacionFecha = reader.GetDateTime(reader.GetOrdinal("ModificacionFecha")),
                };
                objProductosList.Add(objProducto);
            }
            reader.Close();
            objConexion.MtCerrarConexion();

            return objProductosList;
        }
    }
}