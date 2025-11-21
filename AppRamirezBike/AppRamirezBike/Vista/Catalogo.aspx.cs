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
                MtCargarCategorias();

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
                out TotalRegistros,
                MtFiltroCategoria,
                MtFiltroBusqueda
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

            List<int> paginas = new List<int>();
            for (int i = 1; i <= totalPaginas; i++)
            {
                paginas.Add(i);
            }

            // Aquí puedes usar un control como un Repeater o un PlaceHolder para dibujar los botones.
            // Por simplicidad y consistencia, sugiero un segundo Repeater.

            string busquedaActual = MtFiltroBusqueda;
            string categoriaActual = MtFiltroCategoria > 0 ? $"&categoria={MtFiltroCategoria}" : string.Empty;
            string busquedaQuery = !string.IsNullOrEmpty(busquedaActual) ? $"&busqueda={Server.UrlEncode(busquedaActual)}" : string.Empty;

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

        public int MtFiltroCategoria
        {
            get
            {

                if (Request.QueryString["categoria"] != null && int.TryParse(Request.QueryString["categoria"], out int idCat))
                {
                    return idCat;
                }
                return 0;
            }
        }
        private void MtCargarCategorias()
        {
            ClCategoriaLogica objCategoria = new ClCategoriaLogica();
            List<Categoria> listaCategorias = objCategoria.MtObtenerCategorias();

            ddlCategorias.DataSource = listaCategorias;
            ddlCategorias.DataTextField = "nombre";
            ddlCategorias.DataValueField = "idCategoria";
            ddlCategorias.DataBind();

            ddlCategorias.Items.Insert(0, new ListItem("Seleccione Una Categoria", "0"));
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoriaSelect = ddlCategorias.SelectedValue;
            string busquedaActual = MtFiltroBusqueda;
            string url = "/Vista/Catalogo.aspx?pagina=1";

            if (!string.IsNullOrEmpty(categoriaSelect) && categoriaSelect!= "0")
            {
                url += "&categoria=" + categoriaSelect;
            }

            if (!string.IsNullOrEmpty(busquedaActual))
            {
                // Usar Server.UrlEncode para manejar espacios y caracteres especiales.
                url += "&busqueda=" + Server.UrlEncode(busquedaActual);
            }

            Console.WriteLine(url);

            Response.Redirect(url);

        }

        public string MtFiltroBusqueda
        {
            get
            {
                string texto = Request.QueryString["busqueda"];
                return string.IsNullOrEmpty(texto) ? string.Empty : texto.Trim();
            }

        }

        public string BaseUrlFiltros
        {
            get
            {
                // 1. Obtener el filtro de Categoría
                string categoriaQuery = MtFiltroCategoria > 0 ? $"&categoria={MtFiltroCategoria}" : string.Empty;

                // 2. Obtener el filtro de Búsqueda
                string busquedaActual = MtFiltroBusqueda;
                string busquedaQuery = !string.IsNullOrEmpty(busquedaActual) ? $"&busqueda={Server.UrlEncode(busquedaActual)}": string.Empty;

                // Retorna solo los parámetros, listos para ser usados después de 'pagina=X'

                return categoriaQuery + busquedaQuery;
            }
        }
    }
}
