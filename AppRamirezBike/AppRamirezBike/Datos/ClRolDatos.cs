using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClRolDatos
    {
        ClConexion objConexion = new ClConexion();
        
        public List<Rol> MtObtenerRoles()
        {
            List<Rol> listaRoles = new List<Rol>();
            string consulta = $"SELECT idRol,nombre FROM rol";

            SqlCommand comando = new SqlCommand(consulta, objConexion.MtAbrirConexion());
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Rol objRoles = new Rol();
                
                    objRoles.idRol = reader.GetInt32(reader.GetOrdinal("idRol"));
                    objRoles.nombre = reader.GetString(reader.GetOrdinal("nombre"));   
                
                listaRoles.Add(objRoles);
            }
            reader.Close();
            objConexion.MtCerrarConexion();
            return listaRoles;

        }
    }
}