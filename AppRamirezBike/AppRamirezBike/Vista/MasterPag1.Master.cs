using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
	public partial class Site1 : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
	          if(Context.User != null && Context.User.Identity.IsAuthenticated)
			{
				btnLogout.Visible = true;

			}
			else
			{
				btnLogout.Visible = false;
			}

		}

        protected void btnLogout_Click(object sender, EventArgs e)
        {
			FormsAuthentication.SignOut();

			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
			cookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie);


			Response.Redirect("/Vista/Login.aspx");

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del TextBox de la Master Page (usando .Text porque ahora es asp:TextBox)
            string textoBusqueda = txtBusqueda.Text.Trim();

            // 1. Obtener la categoría actual (si existe en la URL de la página hija)
            string categoriaActual = Request.QueryString["categoria"];

            // 2. Construir la URL base. Siempre volvemos a la página 1 al buscar.
            string url = "/Vista/Catalogo.aspx?pagina=1";

            // 3. Preservar la Categoría si estaba activa
            if (!string.IsNullOrEmpty(categoriaActual))
            {
                // Se añade la categoría al URL
                url += "&categoria=" + categoriaActual;
            }

            // 4. Agregar el término de búsqueda si existe
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                // Se añade la búsqueda, codificada para manejar espacios y caracteres especiales
                url += "&busqueda=" + Server.UrlEncode(textoBusqueda);
            }

            // Redirigir a la URL completa con todos los filtros
            Response.Redirect(url);
        }
    }
}