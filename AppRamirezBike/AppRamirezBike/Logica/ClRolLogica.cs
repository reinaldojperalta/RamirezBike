using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClRolLogica
    {
        ClRolDatos objDatos = new ClRolDatos();

        public List<Rol> MtObtenerRoles()
        {
            List<Rol> listaRoles = objDatos.MtObtenerRoles();
            return listaRoles;
        }
    }
}