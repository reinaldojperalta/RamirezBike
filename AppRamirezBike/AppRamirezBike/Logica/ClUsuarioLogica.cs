using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AppRamirezBike.Logica
{
    public class ClUsuarioLogica
    {
        public bool MtRegistrarUsuario(Usuario oUsuario)
        {
            ClUsuarioDatos oUsuarioD = new ClUsuarioDatos();
            int filasAfectadas = oUsuarioD.MtRegistrarUsuario(oUsuario);
            return filasAfectadas == 1;
     
        }
        public bool MtVerificarDocumentoExistente(string documento)
        {
            ClUsuarioDatos oUsuarioD = new ClUsuarioDatos();
            return oUsuarioD.MtVerificarDocumentoExistente(documento);
        }

        public bool MtVerificarLogin(string correo, string claveIngresada)
        {
            ClUsuarioDatos oUsuarioD = new ClUsuarioDatos();
            bool confirmacion = oUsuarioD.MtVerificarLogin(correo,claveIngresada);
            return confirmacion;
        }
        
        public int MtIniciarSesionYCrearCookie(string correo, string clave)
        {
            ClUsuarioDatos objDatos = new ClUsuarioDatos();

            // 1.  aca verificamos las credenciales osea que si llega un false ps entra
            if (!objDatos.MtVerificarLogin(correo, clave))
            {
                return 0;
            }

            
            Usuario objUsuario = objDatos.MtBuscarCorreo(correo);

            // 3. Crear una instancia de cookie con la clave: DatosUsuario
            HttpCookie cookie = new HttpCookie("DatosUsuario");

            // le da un campo idrol a la cookie almasenada como texto
            cookie.Values["idRol"] = objUsuario.idRol.ToString();

            cookie.HttpOnly = true; // la proteccion literalmente dice solo para http
            cookie.Expires = DateTime.Now.AddDays(1);

            // esta cookie es nuestra tarjeta la que nos dira si el usuario logueado puede ingresar a dashboard
            HttpContext.Current.Response.Cookies.Add(cookie);

            // esta cookie es la que nos confirma que el usario esta logueado
            FormsAuthentication.SetAuthCookie(correo, false);

            // 4. Devolver el ID del Rol para la redirección
            return objUsuario.idRol;
        }
    }
}