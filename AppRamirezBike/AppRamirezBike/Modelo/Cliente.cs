using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Modelo
{
    public class Cliente
    {

        public int idCliente { get; set; }
        public string tipoDocumento { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public DateTime fechaCreacion { get; set; }
        // Se podría agregar un campo de dirección si es necesario para el envío
    }
}

