using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClProductosDashboardLogica
    {
        // Instancia la nueva clase de Datos
        ClProductosDashboardDatos datos = new ClProductosDashboardDatos();

        // 1. LISTAR
        public List<Producto> ListarTodosProductos()
        {
            return datos.MtListarProductos();
        }

        // 2. OBTENER POR ID
        public Producto ObtenerProductoPorId(int id)
        {
            return datos.MtObtenerPorId(id);
        }

        // 3. GUARDAR (Lógica para decidir Insertar o Actualizar)
        public string GuardarProducto(Producto obj)
        {
            // Si el ID es 0, es un nuevo registro
            if (obj.idProducto == 0)
            {
                return datos.MtInsertarProducto(obj);
            }
            // Si el ID es > 0, es una actualización
            else
            {
                return datos.MtActualizarProducto(obj);
            }
        }

        // 4. ELIMINAR
        public string EliminarProducto(int id)
        {
            return datos.MtEliminarProducto(id);
        }
    }
}
