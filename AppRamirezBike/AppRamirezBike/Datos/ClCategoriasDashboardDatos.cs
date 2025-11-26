using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClCategoriasDashboardDatos
    {
        // Método para listar todas las categorías (necesario para el dropdown)
        public List<Categoria> MtListarCategorias()
        {
            List<Categoria> objCategoriasList = new List<Categoria>();
            ClConexion objConexion = new ClConexion();
            SqlConnection conex = null;

            // Consulta SQL para obtener la lista de categorías
            // Nota: Asumo que las categorías también tienen estado = true
            string consulta = "SELECT idCategoria, nombre FROM dbo.Categoria";

            conex = objConexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand(consulta, conex);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Mapeo del registro a la clase Categoria (con camelCase)
                Categoria objCategoria = new Categoria
                {
                    // Usando GetInt32 o GetOrdinal para mejor rendimiento y seguridad de tipo
                    idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria")),
                    nombre = reader["nombre"].ToString()
                };
                objCategoriasList.Add(objCategoria);
            }

            reader.Close();
            objConexion.MtCerrarConexion();

            return objCategoriasList;
        }
    }
}