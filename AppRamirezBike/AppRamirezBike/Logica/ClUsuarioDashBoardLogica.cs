using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClUsuarioDashBoardLogica
    {
        Datos.ClUsuarioDashBoardDatos objUsuarioDashBoardDatos = new Datos.ClUsuarioDashBoardDatos();
        public List<Modelo.UsuarioDashBoard> MtListarUsuariosDashBoard()
        {
            return objUsuarioDashBoardDatos.MtListarUsuariosDashBoard();
        }
    }
}