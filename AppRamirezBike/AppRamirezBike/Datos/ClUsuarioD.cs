using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using AppRamirezBike.Modelo;

namespace AppRamirezBike.Datos
{
	public class ClUsuarioD
	{
		ClConexion oConexion = new ClConexion();

		public int MtRegistrarUsuario(ClUsuario oUsuario)
		{
            string consulta = $"INSERT INTO Usuario (tipoDocumento, documento, nombres, apellidos, telefono, email, clave) " +
            $"VALUES ('{oUsuario.tipoDocumento},{oUsuario.documento}', '{oUsuario.nombres}', '{oUsuario.apellidos}', " +
            $"'{oUsuario.telefono}', '{oUsuario.email}', '{oUsuario.contraseña}')";
            SqlCommand comando = new SqlCommand(consulta, oConexion.MtAbrirConexion());
            int filasAfectadas = comando.ExecuteNonQuery();
            oConexion.MtCerrarConexion();

            return filasAfectadas;
        }
	}
}