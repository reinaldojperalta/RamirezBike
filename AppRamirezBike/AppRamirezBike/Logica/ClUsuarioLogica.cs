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

        public Usuario MtLogin(string correo, string clave)
        {
            ClUsuarioDatos oUsuarioD = new ClUsuarioDatos();
            Usuario usuario = oUsuarioD.MtBuscarCorreo(correo);

            if (usuario == null)
            {
                return null;
            }

            bool claveCorrecta = HasheoClave.MtVerificarClave(clave, usuario.clave);

            if (!claveCorrecta)
            {
                return null;
            }

            // Aquí SI trae idUsuario, nombre, email, etc.
            return usuario;
        }
        public int ObtenerIdPorEmail(string correo)
        {
            ClUsuarioDatos datos = new ClUsuarioDatos();
            return datos.ObtenerIdPorEmail(correo);
        }

        public int MtIniciarSesionYCifrarRol(string correo, string clave)
        {
            ClUsuarioDatos objDatos = new ClUsuarioDatos();

            // 1. Verificar Credenciales
            if (!objDatos.MtVerificarLogin(correo, clave))
            {
                return 0; // Fallo de credenciales
            }

            // 2. Obtener Datos del Usuario
            Usuario objUsuario = objDatos.MtBuscarCorreo(correo);
            int idRol = objUsuario.idRol;

            // 3. Crear el Ticket de Autenticación Personalizado (¡La única cookie!)
            if (idRol > 0)
            {
                // 🔒 Cifrar el Rol y guardarlo como datos de usuario del Ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, // Versión del ticket
                    correo, // Nombre de usuario (visible para User.Identity.Name)
                    DateTime.Now, // Fecha de emisión
                    DateTime.Now.AddDays(1), // Fecha de expiración (igual que tu cookie anterior)
                    false, // No persistente (false), aunque la expiración de arriba manda
                    idRol.ToString() // ⬅️ ¡Aquí guardamos el idRol en la sección de datos!
                );

                // 4. Cifrar el Ticket y crear la Cookie
                string encTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                // 5. Establecer la cookie de autenticación (HttpOnly es por defecto)
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            // 6. Devolver el ID del Rol para la redirección
            return idRol;
        }

        public string MtInhabilitarUsuario(int idUsuario)
        {
            ClUsuarioDatos objDatos = new ClUsuarioDatos();
            return objDatos.MtInhabilitarUsuario(idUsuario);
        }

        public Usuario MtObtenerUsuarioPorId(int idUsuario)
        {
            ClUsuarioDatos objDatos = new ClUsuarioDatos();
            return objDatos.MtObtenerUsuarioPorId(idUsuario);
        }

        public string MtActualizarUsuario(Usuario oUsuario)
        {
            ClUsuarioDatos objDatos = new ClUsuarioDatos();
            return objDatos.MtActualizarUsuario(oUsuario);
        }
    }
}