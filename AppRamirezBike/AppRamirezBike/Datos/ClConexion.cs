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

            objConexion = new SqlConnection("Data Source=LAPTOP-6LQPP2A5\\SQLEXPRESS;Initial Catalog=dbRamirezBike;Integrated Security=True;");

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