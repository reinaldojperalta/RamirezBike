using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
	public class ClUsuario
	{
        public int idUsuario { get; set; }
        public string tipoDocumento { get; set; }
        public string documento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email  { get; set; }
        public string contraseña  { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
    }
}