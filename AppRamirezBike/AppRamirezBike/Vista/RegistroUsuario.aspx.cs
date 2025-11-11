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
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MtCargarRoles();
            }
        }

        private void MtCargarRoles()
        {
            ClRolLogica objRolLogica = new ClRolLogica();
            List<Rol> listaRoles = objRolLogica.MtObtenerRoles();
            ddlRol.DataSource = listaRoles;
            ddlRol.DataTextField = "nombre";
            ddlRol.DataValueField = "idRol";
            ddlRol.DataBind();

            ddlRol.Items.Insert(0, new ListItem("Seleccione Un Rol", ""));
        }
        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            int idRol = int.Parse(ddlRol.SelectedValue);
            ClUsuarioLogica objUsuario = new ClUsuarioLogica();
            string documentoIngresado = txtDocumento.Text.Trim();

            if (objUsuario.MtVerificarDocumentoExistente(documentoIngresado))
            {
                string alertaJS = "Swal.fire('Error de Registro', 'El número de documento " + documentoIngresado + " ya se encuentra registrado. Por favor, verifique.','error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "swalAlert", alertaJS, true);
                return;
            }

            Usuario nuevoUsuario = new Usuario()
            {
                tipoDocumento = ddlTipoDocumento.SelectedValue.Trim(),
                documento = documentoIngresado,
                nombre = txtNombres.Text.Trim(),
                apellido = txtApellidos.Text.Trim(),
                email = txtEmail.Text.Trim(),
                telefono = txtTelefono.Text.Trim(),
                clave = txtClave.Text.Trim(),
                idRol = idRol
            };

            bool registroExitoso = objUsuario.MtRegistrarUsuario(nuevoUsuario);
            if (registroExitoso)
            {
                FormsAuthentication.SetAuthCookie(txtEmail.Text.Trim(), false);
                Response.Redirect("/Vista/Catalogo.aspx");
            }
            else
            {
                string alertaJS = "Swal.fire('Error', 'No se pudo completar el registro del usuario. Intente Nuevamente.','error');";
                ScriptManager.RegisterStartupScript(this, GetType(), "swalError", alertaJS, true);
            }

        }
    }
}