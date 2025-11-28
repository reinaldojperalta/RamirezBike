using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
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
            string email = txtEmail.Text.Trim();
            string password = txtClave.Text;

            ClUsuarioLogica objLogica = new ClUsuarioLogica();


            // Llamamos al nuevo método que hace el login y cifra el rol
            int idRol = objLogica.MtIniciarSesionYCifrarRol(email, password);

            // 1. Verificar si el login fue exitoso (idRol > 0)
            if (idRol > 0)
            {
                // 2. Lógica de Redirección 
                if (idRol == 1 || idRol == 2) // Admin o Empleado

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
>>>>>>> 3b039c98cb0b4fb1a228317d1785a4116ff5234d
                {
                    Response.Redirect("~/Vista/admin/Administracion.aspx");
                }
                else // Cliente (cualquier otro)
                {
                    Response.Redirect("~/Vista/Catalogo.aspx");
                }
            }
            else
            {
                // Si la lógica devuelve 0 (fallo de credenciales)
                lblMensaje.Text = "Usuario o contraseña incorrectos.";
            }
        }
    }
}