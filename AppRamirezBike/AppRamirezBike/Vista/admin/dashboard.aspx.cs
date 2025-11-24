using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppRamirezBike.Vista.admin
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["DatosUsuario"]; // Usamos el nombre de la cookie que creamos

            if (cookie == null)
            {
                // Si la cookie no existe, forzamos la redirección al login
                Response.Redirect("~/Vista/login.aspx");
                return;
            }

            // 2. Intentar leer el ID del Rol
            int idRol = 0;
            string rolString = cookie.Values["idRol"];

            // Validamos que el valor sea un número
            if (!int.TryParse(rolString, out idRol))
            {
                // Si el valor no es válido o está corrupto, forzamos login
                Response.Redirect("~/Vista/login.aspx");
                return;
            }

            // 3. LA VALIDACIÓN DE AUTORIZACIÓN (EL GUARDIA)
            // Si el rol NO es 1 (Admin) Y NO es 2 (Empleado), lo echamos.
            if (idRol != 1 && idRol != 2)
            {
                // ¡ES UN CLIENTE! Lo enviamos de vuelta al Catálogo o a donde pertenezca.
                Response.Redirect("~/Vista/Catalogo.aspx"); // ⬅️ ¡Aquí lo bloqueamos!
            }
        }
    }
}