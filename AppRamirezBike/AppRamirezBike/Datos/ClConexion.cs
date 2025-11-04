using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
    public class ClConexion
    {
        SqlConnection oConexion;

        public ClConexion()
        {
            oConexion = new SqlConnection("");
        }
        public SqlConnection MtAbrirConexion()
        {
            oConexion.Open();
            return oConexion;
        }
        public void MtCerrarConexion()
        {
            oConexion.Close();
        }
    }
}