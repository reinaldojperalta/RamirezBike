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

            string consulta = "SELECT IdProducto, nombre, descripcion, precio, precioProovedor, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria FROM dbo.producto ";
            conex = objConexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand(consulta, conex);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto objProducto = new Producto
                {
                    idProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                    nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader["nombre"].ToString(),
                    descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader["descripcion"].ToString(),
                    imgUrl = reader.IsDBNull(reader.GetOrdinal("imgUrl")) ? string.Empty : reader["imgUrl"].ToString(),
                    precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0 : reader.GetInt32(reader.GetOrdinal("precio")),
                    precioProovedor = reader.IsDBNull(reader.GetOrdinal("precioProovedor")) ? 0 : reader.GetInt32(reader.GetOrdinal("precioProovedor")),
                    stock = reader.IsDBNull(reader.GetOrdinal("stock")) ? 0 : reader.GetInt32(reader.GetOrdinal("stock")),
                    idCategoria = reader.IsDBNull(reader.GetOrdinal("idCategoria")) ? 0 : reader.GetInt32(reader.GetOrdinal("idCategoria")),
                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? false : reader.GetBoolean(reader.GetOrdinal("estado")),
                    CreacionFecha = reader.IsDBNull(reader.GetOrdinal("CreacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreacionFecha")),
                    ModificacionFecha = reader.IsDBNull(reader.GetOrdinal("ModificacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ModificacionFecha"))
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

            List<string> condiciones = new List<string>();

            if (idCategoria > 0)
            {
                condiciones.Add("IdCategoria = @IdCategoria");
            }

            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                condiciones.Add(" (nombre LIKE @TextoBusqueda OR descripcion LIKE @TextoBusqueda OR CAST(idProducto AS VARCHAR(10)) LIKE @TextoBusqueda) ");
            }

            string clausulaWhere = condiciones.Count > 0
                ? " WHERE " + string.Join(" AND ", condiciones)
                : "";

            string consultaTotal = "SELECT COUNT(idProducto) FROM dbo.producto" + clausulaWhere;

            string consultaPaginada = $@"
                SELECT idProducto, nombre, descripcion, precio, precioProovedor, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria
                FROM dbo.producto {clausulaWhere}                 ORDER BY idProducto              OFFSET @Salto ROWS             FETCH NEXT @TamañoPagina ROWS ONLY";

            conex = objConexion.MtAbrirConexion();

            SqlCommand cmdTotal = new SqlCommand(consultaTotal, conex);
            if (idCategoria > 0) { cmdTotal.Parameters.AddWithValue("@IdCategoria", idCategoria); }
            if (!string.IsNullOrEmpty(textoBusqueda)) { cmdTotal.Parameters.AddWithValue("@TextoBusqueda", "%" + textoBusqueda.Trim() + "%"); }
            totalRegistros = (int)cmdTotal.ExecuteScalar();

            SqlCommand cmd = new SqlCommand(consultaPaginada, conex);
            cmd.Parameters.AddWithValue("@Salto", salto);
            cmd.Parameters.AddWithValue("@TamañoPagina", tamañoPagina);
            if (idCategoria > 0) { cmd.Parameters.AddWithValue("@IdCategoria", idCategoria); }
            if (!string.IsNullOrEmpty(textoBusqueda)) { cmd.Parameters.AddWithValue("@TextoBusqueda", "%" + textoBusqueda.Trim() + "%"); }

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Producto objProducto = new Producto
                {
                    idProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                    nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader["nombre"].ToString(),
                    descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader["descripcion"].ToString(),
                    imgUrl = reader.IsDBNull(reader.GetOrdinal("imgUrl")) ? string.Empty : reader["imgUrl"].ToString(),
                    precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0 : reader.GetInt32(reader.GetOrdinal("precio")),
                    precioProovedor = reader.IsDBNull(reader.GetOrdinal("precioProovedor")) ? 0 : reader.GetInt32(reader.GetOrdinal("precioProovedor")),
                    stock = reader.IsDBNull(reader.GetOrdinal("stock")) ? 0 : reader.GetInt32(reader.GetOrdinal("stock")),
                    idCategoria = reader.IsDBNull(reader.GetOrdinal("idCategoria")) ? 0 : reader.GetInt32(reader.GetOrdinal("idCategoria")),
                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? false : reader.GetBoolean(reader.GetOrdinal("estado")),
                    CreacionFecha = reader.IsDBNull(reader.GetOrdinal("CreacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreacionFecha")),
                    ModificacionFecha = reader.IsDBNull(reader.GetOrdinal("ModificacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ModificacionFecha"))
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
            SqlConnection conn = conexion.MtAbrirConexion();

            string query = "SELECT idProducto, nombre, descripcion, precio, precioProovedor, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria FROM Producto WHERE idProducto = @id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            Producto producto = null;

            if (reader.Read())
            {
                producto = new Producto
                {
                    idProducto = (int)reader["idProducto"],
                    nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader["nombre"].ToString(),
                    descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader["descripcion"].ToString(),
                    imgUrl = reader.IsDBNull(reader.GetOrdinal("imgUrl")) ? string.Empty : reader["imgUrl"].ToString(),
                    precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0 : reader.GetInt32(reader.GetOrdinal("precio")),
                    precioProovedor = reader.IsDBNull(reader.GetOrdinal("precioProovedor")) ? 0 : reader.GetInt32(reader.GetOrdinal("precioProovedor")),
                    stock = reader.IsDBNull(reader.GetOrdinal("stock")) ? 0 : reader.GetInt32(reader.GetOrdinal("stock")),
                    idCategoria = reader.IsDBNull(reader.GetOrdinal("idCategoria")) ? 0 : reader.GetInt32(reader.GetOrdinal("idCategoria")),
                    estado = reader.IsDBNull(reader.GetOrdinal("estado")) ? false : reader.GetBoolean(reader.GetOrdinal("estado")),
                    CreacionFecha = reader.IsDBNull(reader.GetOrdinal("CreacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreacionFecha")),
                    ModificacionFecha = reader.IsDBNull(reader.GetOrdinal("ModificacionFecha")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("ModificacionFecha"))
                };
            }

            reader.Close();
            conexion.MtCerrarConexion();

            return producto;
        }

        public Producto MtObtenerPorID(int id)
        {
            return MtObtenerPorId(id);
        }
        public int MtObtenerStockPorId(int idProducto)
        {
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;
            int stock = -1; // Valor predeterminado para indicar error o producto no encontrado

            try
            {
                conex = objConexion.MtAbrirConexion();
                string consulta = "SELECT stock FROM dbo.producto WHERE idProducto = @id";

                using (SqlCommand cmd = new SqlCommand(consulta, conex))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        // Maneja el posible valor NULL (que corregimos antes)
                        stock = Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores de conexión o SQL
                Console.WriteLine("Error al obtener stock: " + ex.Message);
                stock = -1;
            }
            finally
            {
                objConexion.MtCerrarConexion();
            }
            return stock;
        }
    }
}