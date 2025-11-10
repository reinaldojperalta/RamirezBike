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
            string consulta = "INSERT INTO usuario (tipoDocumento, documento, nombres, apellidos, telefono, email, clave, idRol) " +
                          "VALUES (@tipoDoc, @doc, @nombres, @apellidos, @tel, @email, @clave, @idRol)";

            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());

            comando.Parameters.AddWithValue("@tipoDoc", oUsuario.tipoDocumento);
            comando.Parameters.AddWithValue("@doc", oUsuario.documento);
            comando.Parameters.AddWithValue("@nombres", oUsuario.nombres);
            comando.Parameters.AddWithValue("@apellidos", oUsuario.apellidos);
            comando.Parameters.AddWithValue("@tel", oUsuario.telefono);
            comando.Parameters.AddWithValue("@email", oUsuario.email);
            comando.Parameters.AddWithValue("@clave", oUsuario.clave);
            comando.Parameters.AddWithValue("@idRol", oUsuario.idRol);

            int filasAfectadas = comando.ExecuteNonQuery();

            objConexion.MtCerrarConexion();

            return filasAfectadas;
        }
        public bool MtVerificarDocumentoExistente(string documento)
        {
            string consulta = "SELECT 1 WHERE EXISTS (SELECT 1 FROM usuario WHERE documento = @doc)";

            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());
            comando.Parameters.AddWithValue("@doc", documento);

            object resultado = comando.ExecuteScalar();

            objConexion.MtCerrarConexion();

            return resultado != null;
        }
    }
}