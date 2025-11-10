using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string tipoDocumento { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        //importante, asugurarse de que la variable este en varchar(max)
        public string clave { get; set; }
        public int idRol { get; set; }
    }
}