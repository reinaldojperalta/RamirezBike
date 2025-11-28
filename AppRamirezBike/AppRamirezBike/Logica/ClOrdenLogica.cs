using AppRamirezBike.Datos;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppRamirezBike.Logica
{
    public class ClOrdenLogica
    {
        private ClOrdenDatos datos = new ClOrdenDatos();

        // ----------------------------
        // 1️⃣ CREAR ORDEN (OBJETO ORDEN)
        // ----------------------------
        public int CrearOrden(Orden orden)
        {
            if (orden == null) return 0;

            // Aseguramos valores si vienen nulos
            if (orden.FechaCreacion == default(DateTime))
                orden.FechaCreacion = DateTime.Now;

            if (string.IsNullOrEmpty(orden.Estado))
                orden.Estado = "Pendiente";

            if (string.IsNullOrEmpty(orden.MetodoPago))
                orden.MetodoPago = "ePayco";

            // La tabla no acepta NULL en Referencia
            if (string.IsNullOrEmpty(orden.Referencia))
                orden.Referencia = "TEMP-" + Guid.NewGuid().ToString("N");

            return datos.RegistrarOrden(orden);
        }

        // ----------------------------------------------------
        // 2️⃣ CREAR ORDEN USANDO PARÁMETROS (OPCIONAL)
        // ----------------------------------------------------
        public int CrearOrden(int idUsuario, decimal total, string metodoPago)
        {
            Orden orden = new Orden
            {
                IdUsuario = idUsuario,
                Total = total,
                Estado = "Pendiente",
                MetodoPago = metodoPago,
                FechaCreacion = DateTime.Now,
                FechaPago = null,
                Referencia = "TEMP-" + Guid.NewGuid().ToString("N")
            };

            return datos.RegistrarOrden(orden);
        }

        // ---------------------------------
        // 3️⃣ CREAR ORDEN CON DETALLES
        // ---------------------------------
        public int CrearOrdenConDetalles(Orden orden, List<OrdenDetalle> detalles)
        {
            orden.FechaCreacion = DateTime.Now;
            orden.Estado = "Pendiente";

            // Generamos referencia provisional
            if (string.IsNullOrEmpty(orden.Referencia))
                orden.Referencia = "TEMP-" + Guid.NewGuid().ToString("N");

            int idOrden = datos.RegistrarOrden(orden);

            if (idOrden > 0)
            {
                foreach (var d in detalles)
                {
                    d.IdOrden = idOrden;
                    datos.RegistrarDetalle(d);
                }
            }

            return idOrden;
        }

        // ---------------------------------------------
        // 4️⃣ CAMBIAR ESTADO (pagado, fallido, etc.)
        // ---------------------------------------------
        public int CambiarEstado(int idOrden, string estado)
        {
            return datos.ActualizarEstado(idOrden, estado);
        }

        // ---------------------------------------------
        // 5️⃣ CAMBIAR TOTAL (si necesitas recalcular)
        // ---------------------------------------------
        public int CambiarTotal(int idOrden, decimal total)
        {
            return datos.ActualizarTotal(idOrden, total);
        }

        // ---------------------------------------------
        // 6️⃣ GUARDAR REFERENCIA FINAL Epayco
        // ---------------------------------------------
        public int GuardarReferencia(int idOrden, string referencia)
        {
            return datos.ActualizarReferencia(idOrden, referencia);
        }
        public void ActualizarEstadoPorReferencia(string referencia, string estado)
        {
            datos.ActualizarEstadoPorReferencia(referencia, estado);
        }
        public int ActualizarFechaPagoPorReferencia(string referencia, DateTime fechaPago)
        {
            return datos.ActualizarFechaPagoPorReferencia(referencia, fechaPago);
        }

    }
}