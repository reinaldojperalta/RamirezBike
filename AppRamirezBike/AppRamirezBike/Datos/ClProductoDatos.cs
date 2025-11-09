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
            List<Producto> objProductosList = new List<Producto>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            string consulta = "SELECT IdProducto, nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha FROM dbo.producto ";
            conex = objConexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand(consulta, conex);

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

        public List<Producto> MtListarProductosPaginados(int salto, int tamañoPagina, out int totalRegistros)
        {
            List<Producto> objProductosList = new List<Producto>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            // 1. Consulta para obtener el TOTAL de registros (necesario para la paginación)
            string consultaTotal = "SELECT COUNT(IdProducto) FROM dbo.producto";

            // 2. Consulta para obtener los registros de la página actual
            // Es crucial que haya un ORDER BY para que OFFSET y FETCH funcionen.
            string consultaPaginada = @"
        SELECT IdProducto, nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha 
        FROM dbo.producto
        ORDER BY IdProducto -- O cualquier columna de orden, es OBLIGATORIO
        OFFSET @Salto ROWS
        FETCH NEXT @TamañoPagina ROWS ONLY";

            conex = objConexion.MtAbrirConexion();

            // Obtener el Total de Registros
            SqlCommand cmdTotal = new SqlCommand(consultaTotal, conex);
            totalRegistros = (int)cmdTotal.ExecuteScalar();

            // Obtener los Productos Paginados
            SqlCommand cmd = new SqlCommand(consultaPaginada, conex);
            cmd.Parameters.AddWithValue("@Salto", salto);
            cmd.Parameters.AddWithValue("@TamañoPagina", tamañoPagina);

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