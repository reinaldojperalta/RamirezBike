using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AppRamirezBike.Modelo;

namespace AppRamirezBike.Datos
{
    public class ClUsuarioDashBoardDatos
    {
        ClConexion objConexion = new ClConexion();
        public List<UsuarioDashBoard> MtListarUsuariosDashBoard()
        {
            List<UsuarioDashBoard> objUsuarioDashBoard = new List<UsuarioDashBoard>();
            SqlConnection conex = null;

            string consulta = @"
        SELECT u.idUsuario, u.nombre, u.apellido, u.email, u.telefono, r.nombre AS nombreRol
        FROM usuario u
        INNER JOIN rol r ON u.idRol = r.idRol
        ORDER BY u.nombre ASC";

            conex = objConexion.MtAbrirConexion();
            SqlCommand cmd = new SqlCommand(consulta, conex);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {

                UsuarioDashBoard usuario = new UsuarioDashBoard
                {
                    idUsuario = Convert.ToInt32(reader["idUsuario"]),
                    nombreCompleto = reader["nombre"].ToString() + " " + reader["apellido"].ToString(),
                    email = reader["email"].ToString(),
                    telefono = reader["telefono"].ToString(),
                    nombreRol = reader["nombreRol"].ToString()
                };
                objUsuarioDashBoard.Add(usuario);

            }
            reader.Close();
            objConexion.MtCerrarConexion();
            return objUsuarioDashBoard;

        }
    }
}