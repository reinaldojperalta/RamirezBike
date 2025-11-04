using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
	public class ClUsuarioL
	{
        public bool MtRegistrarL(ClUsuario oUsuario)
        {
            ClUsuarioD oUsuarioD = new ClUsuarioD();
            int filas = oUsuarioD.MtRegistrarUsuario(oUsuario);
            return filas == 1;
        }
    }
}