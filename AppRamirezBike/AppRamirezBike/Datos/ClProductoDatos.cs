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

        public List<Producto> MtListarProductosPaginados(int salto, int tamañoPagina, out int totalRegistros, int idCategoria, string textoBusqueda)
        {
            List<Producto> objProductosList = new List<Producto>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            // 1. Construcción Dinámica de la Cláusula WHERE y Parámetros
            List<string> condiciones = new List<string>();

            // Condición para filtrar por Categoría
            if (idCategoria > 0)
            {
                condiciones.Add("IdCategoria = @IdCategoria");
            }
            // Condición para buscar por Nombre o Descripción
            // Se usa TRIM para manejar espacios en blanco al inicio/final del texto de búsqueda
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                // Se agrupan las condiciones de búsqueda por texto con OR
                condiciones.Add("(nombre LIKE @TextoBusqueda OR descripcion LIKE @TextoBusqueda)");
            }

            // Unir las condiciones con ' AND ' y añadir ' WHERE ' al inicio si hay condiciones.
            string clausulaWhere = condiciones.Count > 0
                ? " WHERE " + string.Join(" AND ", condiciones)
                : "";

            // 1. Consulta para obtener el TOTAL de registros (necesario para la paginación)
            string consultaTotal = "SELECT COUNT(idProducto) FROM dbo.producto" + clausulaWhere;

            // 2. Consulta para obtener los registros de la página actual
            // Es crucial que haya un ORDER BY para que OFFSET y FETCH funcionen.
            string consultaPaginada = $@"
                SELECT idProducto, nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria
                FROM dbo.producto
                {clausulaWhere}  
                ORDER BY idProducto
                OFFSET @Salto ROWS
                FETCH NEXT @TamañoPagina ROWS ONLY";

            conex = objConexion.MtAbrirConexion();

            // Obtener el Total de Registros
            SqlCommand cmdTotal = new SqlCommand(consultaTotal, conex);

            // Asignar parámetros al comando de TOTAL (solo los que se usaron en la clausula WHERE)
            if (idCategoria > 0)
            {
                cmdTotal.Parameters.AddWithValue("@IdCategoria", idCategoria);
            }
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                // Se añade % para que el LIKE funcione (ej: %samsung%)
                cmdTotal.Parameters.AddWithValue("@TextoBusqueda", "%" + textoBusqueda.Trim() + "%");
            }

            totalRegistros = (int)cmdTotal.ExecuteScalar();

            // Obtener los Productos Paginados
            SqlCommand cmd = new SqlCommand(consultaPaginada, conex);
            cmd.Parameters.AddWithValue("@Salto", salto);
            cmd.Parameters.AddWithValue("@TamañoPagina", tamañoPagina);

            // Asignar parámetros de filtro/búsqueda (los mismos que para cmdTotal)
            if (idCategoria > 0)
            {
                cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
            }
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                // Se añade % para que el LIKE funcione (ej: %samsung%)
                cmd.Parameters.AddWithValue("@TextoBusqueda", "%" + textoBusqueda.Trim() + "%");
            }

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
                    idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"))
                };
                objProductosList.Add(objProducto);
            }
            reader.Close();
            objConexion.MtCerrarConexion();

            return objProductosList;
        }
        public Producto MtObtenerPorId(int id)
        {
            ClConexion conexion = new ClConexion();
            SqlConnection conn = conexion.MtAbrirConexion(); // ABRE Y DEVUELVE CONEXIÓN

            string query = "SELECT idProducto, nombre, descripcion, precio, imgUrl, stock, estado FROM Producto WHERE idProducto = " + id;

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Producto producto = null;

            if (reader.Read())
            {
                producto = new Producto();
                producto.idProducto = (int)reader["idProducto"];
                producto.nombre = reader["nombre"].ToString();
                producto.descripcion = reader["descripcion"].ToString();
                producto.precio = Convert.ToInt32(reader["precio"]);
                producto.imgUrl = reader["imgUrl"].ToString();
                producto.stock = (int)reader["stock"];
                producto.estado = (bool)reader["estado"];
            }

            conexion.MtCerrarConexion(); // CIERRA

            return producto; // SI NO ENCUENTRA → NULL (lógica lo maneja)
        }
      

    }
}