using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
	public class ClCategoriaDatos
	{
		ClConexion objConexion = new ClConexion();

		public List<Categoria> MtListarCategorias()
		{
			List<Categoria> listaCategorias = new List<Categoria>();
			string consulta = "SELECT * FROM categoria";
			SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());
			SqlDataReader reader = comando.ExecuteReader();

			while (reader.Read())
			{
				Categoria objCategoria = new Categoria();

				objCategoria.idCategoria = reader.GetInt32(reader.GetOrdinal("idCategoria"));
				objCategoria.nombre = reader.GetString(reader.GetOrdinal("nombre"));

				listaCategorias.Add(objCategoria);
			}
			reader.Close();
			objConexion.MtCerrarConexion();
			return listaCategorias;
		}
	}
}