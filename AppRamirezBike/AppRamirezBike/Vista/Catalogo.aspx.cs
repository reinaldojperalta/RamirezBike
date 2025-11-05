using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;

namespace AppRamirezBike.Vista
{
	public partial class Catalogo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				CargarCatalogo();
			}
		}
		private void CargarCatalogo()
		{
			ClProductoLogica objProductoLogica = new ClProductoLogica();
			List<Producto> productoLista = objProductoLogica.MtDatosVistaProducto();
			rptProducto.DataSource = productoLista;
			rptProducto.DataBind();
		}
	}
}