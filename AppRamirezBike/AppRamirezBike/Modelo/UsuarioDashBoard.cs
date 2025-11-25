using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class UsuarioDashBoard
    {
        public int idUsuario { get; set; }
        public string nombreCompleto { get; set; } 
        public string email { get; set; }
        public string telefono { get; set; }
        public string nombreRol { get; set; }
    }
}