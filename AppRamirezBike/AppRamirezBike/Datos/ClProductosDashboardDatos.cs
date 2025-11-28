using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClProductosDashboardDatos
    {
        public string MtInsertarProducto(Producto objProducto)
        {
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = objConexion.MtAbrirConexion();

            string consulta = "INSERT INTO dbo.producto (nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria) " +
                              "VALUES (@nombre, @descripcion, @precio, @imgUrl, @stock, @estado, GETDATE(), GETDATE(), @idCategoria)";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            cmd.Parameters.AddWithValue("@nombre", objProducto.nombre);
            cmd.Parameters.AddWithValue("@descripcion", objProducto.descripcion);
            cmd.Parameters.AddWithValue("@precio", objProducto.precio);
            cmd.Parameters.AddWithValue("@imgUrl", objProducto.imgUrl);
            cmd.Parameters.AddWithValue("@stock", objProducto.stock);
            cmd.Parameters.AddWithValue("@estado", true); // O objProducto.estado si lo mandas desde el form
            cmd.Parameters.AddWithValue("@idCategoria", objProducto.idCategoria);

            cmd.ExecuteNonQuery();

            objConexion.MtCerrarConexion();

            return "Producto registrado correctamente";
        }

        public string MtActualizarProducto(Producto objProducto)
        {
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = objConexion.MtAbrirConexion();

            string consulta = "UPDATE dbo.producto SET " +
                              "nombre = @nombre, " +
                              "descripcion = @descripcion, " +
                              "precio = @precio, " +
                              "imgUrl = @imgUrl, " +
                              "stock = @stock, " +
                              "idCategoria = @idCategoria, " +
                              "ModificacionFecha = GETDATE() " +
                              "WHERE idProducto = @idProducto";

            SqlCommand cmd = new SqlCommand(consulta, conex);
            cmd.Parameters.AddWithValue("@idProducto", objProducto.idProducto);
            cmd.Parameters.AddWithValue("@nombre", objProducto.nombre);
            cmd.Parameters.AddWithValue("@descripcion", objProducto.descripcion);
            cmd.Parameters.AddWithValue("@precio", objProducto.precio);
            cmd.Parameters.AddWithValue("@imgUrl", objProducto.imgUrl);
            cmd.Parameters.AddWithValue("@stock", objProducto.stock);
            cmd.Parameters.AddWithValue("@idCategoria", objProducto.idCategoria);

            cmd.ExecuteNonQuery();

            objConexion.MtCerrarConexion();

            return "Producto actualizado correctamente";
        }

        public string MtEliminarProducto(int id)
        {
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = objConexion.MtAbrirConexion();

            string consulta = "DELETE FROM dbo.producto WHERE idProducto = " + id;

            SqlCommand cmd = new SqlCommand(consulta, conex);

            cmd.ExecuteNonQuery();

            objConexion.MtCerrarConexion();

            return "Producto eliminado correctamente";
        }

        public List<Producto> MtListarProductos()
        {
            List<Producto> objProductosList = new List<Producto>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            // Nota: Aquí se traen todos los campos necesarios para la tabla y el modelo
            string consulta = "SELECT IdProducto, nombre, descripcion, precio, imgUrl, stock, estado, CreacionFecha, ModificacionFecha, idCategoria FROM dbo.producto";
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
                    precio = Convert.ToInt32(reader["precio"]), // O Decimal/Double si tu BD usa ese tipo
                    imgUrl = reader["imgUrl"].ToString(),
                    stock = reader.GetInt32(reader.GetOrdinal("stock")),
                    estado = reader.GetBoolean(reader.GetOrdinal("estado")),
                    CreacionFecha = reader.GetDateTime(reader.GetOrdinal("CreacionFecha")),
                    ModificacionFecha = reader.GetDateTime(reader.GetOrdinal("ModificacionFecha")),
                    idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria")) // Asegúrate de que este campo exista
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

            string query = "SELECT idProducto, nombre, descripcion, precio, imgUrl, stock, estado, idCategoria FROM Producto WHERE idProducto = " + id; // Concatenación simple

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Producto producto = null;

            if (reader.Read())
            {
                producto = new Producto();
                producto.idProducto = (int)reader["idProducto"];
                producto.nombre = reader["nombre"].ToString();
                producto.descripcion = reader["descripcion"].ToString();
                producto.precio = Convert.ToInt32(reader["precio"]); // O el tipo de dato correcto
                producto.imgUrl = reader["imgUrl"].ToString();
                producto.stock = (int)reader["stock"];
                producto.estado = (bool)reader["estado"];
                producto.idCategoria = (int)reader["idCategoria"];
            }
            reader.Close();
            conexion.MtCerrarConexion();

            return producto;
        }
    }
}