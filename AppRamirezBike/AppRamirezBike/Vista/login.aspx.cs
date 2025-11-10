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
                // Redirige al catálogo si ya inició sesión
                //Response.Redirect("Vista/Catalogo.aspx");
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtEmail.Text.Trim();
            string password = txtClave.Text;

            ClUsuarioLogica objLogica = new ClUsuarioLogica();

            if (objLogica.MtVerificarLogin(usuario, password))
            {
                FormsAuthentication.SetAuthCookie(usuario, false);

                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl != null)
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