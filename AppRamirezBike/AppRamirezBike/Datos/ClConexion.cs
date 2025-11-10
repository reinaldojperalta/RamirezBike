using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Datos
{
	public class ClConexion
	{
        SqlConnection objConexion;

        public ClConexion()
        {
            objConexion = new SqlConnection("Data Source=DESKTOP-8842IPF;Initial Catalog=dbPruebaProyectoMañana;Integrated Security=True;TrustServerCertificate=True");
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