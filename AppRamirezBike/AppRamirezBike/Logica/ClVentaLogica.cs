
using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppRamirezBike.Logica
{
    public class ClVentaLogica
    {
        private ClVentaDatos objVentaDatos = new ClVentaDatos();
        private ClProductoDatos objProductoDatos = new ClProductoDatos(); 

      
        public int MtProcesarVenta(int idCliente, int idUsuario, List<DetalleVenta> detallesCarrito, out string mensajeError)
        {
            mensajeError = string.Empty;

            // 1. VALIDACIÓN DE NEGOCIO: Stock de Productos
            foreach (var detalle in detallesCarrito)
            {
                int stockDisponible = objProductoDatos.MtObtenerStockPorId(detalle.idProducto);

                if (stockDisponible == -1)
                {
                    mensajeError = $"Error de base de datos al verificar el stock del producto ID {detalle.idProducto}.";
                    return 0;
                }

                if (detalle.cantidad > stockDisponible)
                {
                    mensajeError = $"Stock insuficiente para el producto ID {detalle.idProducto}. Disponible: {stockDisponible}, Solicitado: {detalle.cantidad}.";
                    return 0; 
                }
            }

            // Si llegamos aquí, el stock es suficiente.

            
            decimal subtotal = detallesCarrito.Sum(d => d.subtotal);
            decimal totalVentaConImpuesto = subtotal * (1 + 0.19M);

            Venta nuevaVenta = new Venta
            {
                idCliente = idCliente,
                idUsuario = idUsuario,
                totalVenta = totalVentaConImpuesto,
            };

            int idVentaGenerada = 0;
            try
            {
                idVentaGenerada = objVentaDatos.MtRegistrarVentaTransaccional(
                    nuevaVenta,
                    detallesCarrito
                );
            }
            catch (Exception ex)
            {
                mensajeError = "Fallo en la transacción de la Base de Datos: " + ex.Message;
                return 0;
            }

            if (idVentaGenerada == 0)
            {
                mensajeError = "La venta no fue registrada. Verifique logs del servidor.";
            }

            return idVentaGenerada;
        }
    }
}