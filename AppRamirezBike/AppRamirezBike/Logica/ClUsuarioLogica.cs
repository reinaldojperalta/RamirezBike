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
    }
}