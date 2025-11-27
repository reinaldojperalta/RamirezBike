using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}