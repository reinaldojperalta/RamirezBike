using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista.admin
{
    public partial class dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Vista/login.aspx");
                return;
            }

            // 2. Obtener el ticket de Forms Authentication
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                // Esto puede ocurrir si el usuario manipula la cookie, aunque la primera verificación fallaría.
                FormsAuthentication.SignOut(); // Forzamos el cierre
                Response.Redirect("~/Vista/login.aspx");
                return;
            }

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            // 3. Obtener el idRol de los datos del ticket
            int idRol = 0;
            if (!int.TryParse(ticket.UserData, out idRol))
            {
                // Si no podemos leer el rol cifrado, es un ataque o una corrupción.
                FormsAuthentication.SignOut();
                Response.Redirect("~/Vista/login.aspx");
                return;
            }

            // 4. LA VALIDACIÓN DE AUTORIZACIÓN (EL GUARDIA)
            if (idRol != 1 && idRol != 2)
            {
                // ¡ES UN CLIENTE! Lo enviamos de vuelta al Catálogo.
                Response.Redirect("~/Vista/Catalogo.aspx");
            }
        }
    }
}