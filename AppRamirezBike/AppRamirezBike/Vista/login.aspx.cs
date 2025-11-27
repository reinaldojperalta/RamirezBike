using AppRamirezBike.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Catalogo.aspx");
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtEmail.Text.Trim();
            string password = txtClave.Text;

            ClUsuarioLogica objLogica = new ClUsuarioLogica();

            // Llama al método correcto
            var usuarioObj = objLogica.MtLogin(usuario, password);

            if (usuarioObj != null)
            {
                // Mantiene autenticación por cookie
                FormsAuthentication.SetAuthCookie(usuarioObj.email, false);

                // Guardar datos clave en sesión
                Session["idUsuario"] = usuarioObj.idUsuario;
                Session["emailUsuario"] = usuarioObj.email;

                // Redirección igual que antes
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
                else
                {
                    Response.Redirect("Catalogo.aspx");
                }
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}