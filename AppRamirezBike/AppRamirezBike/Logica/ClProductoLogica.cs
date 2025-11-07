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
        public Producto ObtenerProductoPorId(int id)
        {
            ClProductoDatos datos = new ClProductoDatos();
            List<Producto> lista = datos.MtListarProductos();

            foreach (Producto producto in lista)
            {
                if (producto.idProducto == id)// el if confirmara si existe el id encontrado y lo retornara
                    return producto;
            }
            return new Producto
            {
                idProducto = id,
                nombre = "Producto de Prueba",
                precio = 999999,
                imgUrl = "img/default.jpg",
                descripcion = "Este producto es de prueba porque no se encontró el original",
                stock = 99,
                estado = true,
                CreacionFecha = DateTime.Now,
                ModificacionFecha = DateTime.Now
            };
        }
        
    }
}