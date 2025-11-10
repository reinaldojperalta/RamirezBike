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


            public List<Producto> MtDatosVistaProducto(int salto, int tamañoPagina, out int totalRegistros, int idCategoria)
            {
                ClProductoDatos objProductoDatos = new ClProductoDatos();

              
                List<Producto> productosPaginados = objProductoDatos.MtListarProductosPaginados(
                    salto,
                    tamañoPagina,
                    out totalRegistros,
                    idCategoria
                );

            return productosPaginados;
        }



        public Producto ObtenerProductoPorId(int id)
        {
            ClProductoDatos datos = new ClProductoDatos();
            Producto producto = datos.MtObtenerPorId(id);

            if (producto == null)
            {
                producto = new Producto();
                producto.idProducto = id;
                producto.nombre = "Producto de Prueba";
                producto.precio = 999999;
                producto.imgUrl = "img/default.jpg";
                producto.descripcion = "Este producto es de prueba";
                producto.stock = 99;
                producto.estado = true;

            }

            return producto;
        }

    }
}
