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

        private ClProductoDatos objProductoDatos = new ClProductoDatos();
        public List<Producto> MtDatosVistaProducto(int salto, int tamañoPagina, out int totalRegistros, int idCategoria, string busqueda)
        {
            
            List<Producto> productosPaginados = objProductoDatos.MtListarProductosPaginados(
                salto,
                tamañoPagina,
                out totalRegistros,
                idCategoria,
                busqueda
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
                producto.precioProovedor = 999999;
                producto.imgUrl = "img/default.jpg";
                producto.descripcion = "Este producto es de prueba";
                producto.stock = 99;
                producto.estado = true;

            }

            return producto;
        }
        public List<Producto> MtObtenerProductos()
        {
            ClProductoDatos datos = new ClProductoDatos();
            return datos.MtListarProductos();
        }

        public bool MtRegistrarProducto(Producto producto, out string mensajeError)
        {
            mensajeError = string.Empty;

            // --- 1. Validaciones de Negocio para el Registro ---
            if (string.IsNullOrEmpty(producto.nombre) || producto.nombre.Trim().Length < 3)
            {
                mensajeError = "El nombre es obligatorio y debe tener al menos 3 caracteres.";
                return false;
            }
            if (producto.precio <= 0)
            {
                mensajeError = "El precio de venta debe ser un valor positivo.";
                return false;
            }
            // Puedes añadir validaciones de que la Categoría (idCategoria) sea válida aquí.

            // --- 2. Llamada a la Capa de Datos ---
            return objProductoDatos.MtRegistrarProducto(producto, out mensajeError);
        }

        
        public bool MtActualizarProducto(Producto producto, out string mensajeError)
        {
            mensajeError = string.Empty;

            // --- 1. Validaciones de Negocio para la Edición (CRÍTICO) ---
            if (producto.idProducto <= 0)
            {
                mensajeError = "Se requiere un ID de producto válido para la actualización.";
                return false;
            }
            if (string.IsNullOrEmpty(producto.nombre) || producto.nombre.Trim().Length < 3)
            {
                mensajeError = "El nombre del producto es obligatorio y debe tener al menos 3 caracteres.";
                return false;
            }
            if (producto.precio <= 0)
            {
                mensajeError = "El precio de venta debe ser un valor positivo.";
                return false;
            }
            if (producto.stock < 0)
            {
                mensajeError = "El stock no puede ser negativo.";
                return false;
            }

            // --- 2. Llamada a la Capa de Datos ---
            return objProductoDatos.MtActualizarProducto(producto, out mensajeError);
        }
    }
}
    
