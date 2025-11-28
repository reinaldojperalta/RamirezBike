using AppRamirezBike.Logica;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace AppRamirezBike.Vista
{
    public partial class ConfirmacionEpayco : System.Web.UI.Page
    {
        ClOrdenLogica logicaOrden = new ClOrdenLogica();

        protected void Page_Load(object sender, EventArgs e)
        {
            string refPayco = Request.QueryString["ref_payco"];

            if (string.IsNullOrEmpty(refPayco))
            {
                lblEstado.Text = "No se recibió referencia de pago";
                lblReferencia.Text = "-";
                lblValor.Text = "-";
                lblFecha.Text = "-";
                return;
            }

            lblReferencia.Text = refPayco;

            try
            {
                // Llamamos al API de ePayco para obtener la info de la transacción
                using (var client = new WebClient())
                {
                    string url = $"https://secure.epayco.co/validation/v1/reference/{refPayco}";
                    string respuesta = client.DownloadString(url);

                    dynamic datos = JsonConvert.DeserializeObject(respuesta);

                    refPayco = datos.data.x_id_invoice;
                    string estadoAPI = datos.data.x_respuesta;
                    string amountStr = datos.data.x_amount;
                    string fechaStr = datos.data.x_transaction_date;

                    decimal amount = 0;
                    decimal.TryParse(Convert.ToString(amountStr), out amount);

                    DateTime fechaPago = DateTime.Now;
                    DateTime.TryParse(Convert.ToString(fechaStr), out fechaPago);

                    // Determinar estado para mostrar y actualizar
                    string estadoFinal = "Desconocido";
                    if (estadoAPI == "Aceptada") estadoFinal = "Pagada";
                    else if (estadoAPI == "Rechazada") estadoFinal = "Rechazada";
                    else if (estadoAPI == "Pendiente") estadoFinal = "Pendiente";
                    else if (estadoAPI == "Fallida") estadoFinal = "Fallida";

                    lblEstado.Text = estadoFinal;
                    lblEstado.CssClass = estadoFinal; // para color
                    lblValor.Text = amount.ToString("C0"); // formato moneda
                    lblFecha.Text = fechaPago.ToString("dd/MM/yyyy HH:mm:ss");

                    // Actualizar base de datos
                    logicaOrden.ActualizarEstadoPorReferencia(refPayco, estadoFinal);
                    if (estadoFinal == "Pagada")
                        logicaOrden.ActualizarFechaPagoPorReferencia(refPayco, fechaPago);
                }
            }
            catch (Exception)
            {
                lblEstado.Text = "Error al verificar el pago";
                lblValor.Text = "-";
                lblFecha.Text = "-";
                // Opcional: loguear ex.Message
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Limpiar carrito en sesión
            Session["Carrito"] = null;

            Session["TotalCarrito"] = null;


            Response.Redirect("Catalogo.aspx");
        }
    }
}