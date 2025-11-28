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
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
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



            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                // Obtener la cadena completa: ej. "15,3"
                string userData = ticket.UserData;

                // 1. Separar la cadena por la coma
                string[] dataParts = userData.Split(',');

                int idUsuario = 0;
                int idRol = 0;

                if (dataParts.Length == 2)
                {
                    // 2. Convertir la primera parte a idUsuario
                    if (int.TryParse(dataParts[0], out idUsuario))
                    {
                        // 3. Convertir la segunda parte a idRol
                        int.TryParse(dataParts[1], out idRol);
                    }
                }
                if (idRol == 0)
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
}