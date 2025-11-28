using System;
using System.Configuration;
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


            objConexion = new SqlConnection(ConfigurationManager.ConnectionStrings["dbRamirezBikeConnection"].ConnectionString);
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