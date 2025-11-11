using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClUsuarioDatos
    {


        ClConexion objConexion = new ClConexion();
        
        public int MtRegistrarUsuario(Usuario oUsuario)
        {
            string consulta = "INSERT INTO usuario (tipoDocumento, documento, nombres, apellidos, telefono, email, clave, idRol) " +
                          "VALUES (@tipoDoc, @doc, @nombre, @apellido, @tel, @email, @clave, @idRol)";

            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());

            comando.Parameters.AddWithValue("@tipoDoc", oUsuario.tipoDocumento);
            comando.Parameters.AddWithValue("@doc", oUsuario.documento);
            comando.Parameters.AddWithValue("@nombre", oUsuario.nombre);
            comando.Parameters.AddWithValue("@apellido", oUsuario.apellido);
            comando.Parameters.AddWithValue("@tel", oUsuario.telefono);
            comando.Parameters.AddWithValue ("@email", oUsuario.email);
            comando.Parameters.AddWithValue("@clave", HasheoClave.MtHashClave(oUsuario.clave));
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

        public Usuario MtBuscarCorreo(string correo)
        {
            Usuario usuarioEncontrado = null;
            string consulta = "SELECT idUsuario, tipoDocumento, documento, nombres, apellidos, telefono, email, clave, idRol " +
                              "FROM usuario WHERE email = @correo";
            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());
            comando.Parameters.AddWithValue("@correo", correo);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                usuarioEncontrado = new Usuario
                {
                    idUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                    tipoDocumento = reader["tipoDocumento"].ToString(),
                    documento = reader["documento"].ToString(),
                    nombre = reader["nombres"].ToString(),
                    apellido = reader["apellidos"].ToString(),
                    telefono = reader["telefono"].ToString(),
                    email = reader["email"].ToString(),
                    clave = reader["clave"].ToString(),
                    idRol = reader.GetInt32(reader.GetOrdinal("idRol"))
                };
            }
            reader.Close();
            objConexion.MtCerrarConexion();
            return usuarioEncontrado;
        }
        public bool MtVerificarLogin(string correo, string claveIngresada)
        {
            Usuario usuario = MtBuscarCorreo(correo);
            if (usuario == null)
            {
                return false; // Usuario no encontrado
            }
            // Verificar la clave ingresada contra el hash almacenado
            return HasheoClave.MtVerificarClave(claveIngresada, usuario.clave);
        }

       

    }
}