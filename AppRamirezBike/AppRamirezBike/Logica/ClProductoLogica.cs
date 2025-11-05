using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppRamirezBike.Modelo;
using AppRamirezBike.Datos;

namespace AppRamirezBike.Logica
{
    public class ClProductoLogica
    {
        public List<Producto> MtDatosVistaProducto()
        {
            ClProductoDatos objProductoDatos = new ClProductoDatos();
            return objProductoDatos.MtListarProductos();
        }
    }
}