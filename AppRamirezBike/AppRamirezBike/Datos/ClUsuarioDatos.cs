using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClUsuarioDatos
    {
        ClConexion objConexion = new ClConexion();
        public int MtRegistrarUsuario(Usuario oUsuario)
        {
            string consulta = $"INSERT INTO Usuario (tipoDocumento, documento, nombres, apellidos, telefono, email, clave) " +
            $"VALUES ('{oUsuario.tipoDocumento}', '{oUsuario.documento}', '{oUsuario.nombres}', '{oUsuario.apellidos}', '{oUsuario.telefono}', '{oUsuario.email}', '{oUsuario.clave}')";

            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());
            int filasAfectadas = comando.ExecuteNonQuery();
            objConexion.MtCerrarConexion();

            return filasAfectadas;
        }
    }
}