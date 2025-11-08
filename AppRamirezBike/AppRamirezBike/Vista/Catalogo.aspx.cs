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
		const int tamañoPagina = 6;
        private int TotalRegistros;

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                int paginaActual = 1;
                if (Request.QueryString["pagina"] != null && int.TryParse(Request.QueryString["pagina"], out int p))
                {
                    paginaActual = p;
                }

                CargarCatalogo(paginaActual);
            }
		}
        private void CargarCatalogo(int indicePagina)
        {
            // 1. Calcular el salto (OFFSET)
            int salto = (indicePagina - 1) * tamañoPagina;

            // 2. Llamar al método de lógica con los parámetros de paginación
            ClProductoLogica objProductoLogica = new ClProductoLogica();

            // La variable TotalRegistros se llena con el parámetro 'out'
            List<Producto> productosDePagina = objProductoLogica.MtDatosVistaProducto(
                salto,
                tamañoPagina,
                out TotalRegistros
            );

            // 3. Llenar el Repeater
            rptProducto.DataSource = productosDePagina;
            rptProducto.DataBind();

            // 4. Generar los controles de paginación
            GenerarPaginacion(indicePagina);
        }
        private void GenerarPaginacion(int paginaActual)
        {
            // Calcular el número total de páginas
            int totalPaginas = (int)Math.Ceiling((double)TotalRegistros / tamañoPagina);

            // Crea un Repeater de Paginación invisible. 
            // ASUMO que tienes un Repeater llamado rptPaginacion en tu HTML (lo crearemos después)

            List<int> paginas = new List<int>();
            for (int i = 1; i <= totalPaginas; i++)
            {
                paginas.Add(i);
            }

            // Aquí puedes usar un control como un Repeater o un PlaceHolder para dibujar los botones.
            // Por simplicidad y consistencia, sugiero un segundo Repeater.

            // *** NOTA: ASUME que tienes un Repeater con ID="rptPaginacion" en tu ASPX ***
           
             rptPaginacion.DataSource = paginas;
             rptPaginacion.DataBind();
        }
        public string EsPaginaActiva(string numeroPagina)
        {
            // Lee la página actual de la QueryString
            string paginaActual = Request.QueryString["pagina"] ?? "1";

            // Si el número de página del Repeater coincide con la página actual, devuelve 'active'
            return numeroPagina == paginaActual ? "active" : string.Empty;
        }
    }
}
