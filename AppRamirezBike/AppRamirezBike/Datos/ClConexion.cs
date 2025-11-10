using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AppRamirezBike.Datos
{
	public class ClConexion
	{
        SqlConnection objConexion;

        public ClConexion()
        {
            string connectionString = "Data Source=LAPTOP-6LQPP2A5\\SQLEXPRESS;Initial Catalog=dbRamirezBike;Integrated Security=True;";
            objConexion = new SqlConnection(connectionString);

        }
        public SqlConnection MtAbrirConexion()
        {
            objConexion.Open();
            return objConexion;
        }

        public void MtCerrarConexion()
        {
            objConexion.Close();
        }
	}
}