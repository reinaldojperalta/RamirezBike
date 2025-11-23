using AppRamirezBike.Logica;
using AppRamirezBike.Modelo;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace AppRamirezBike.Vistas
{
    public partial class GestionProductos : System.Web.UI.Page
    {
        // Instancia de la capa de lógica para interactuar con los datos
        private ClProductoLogica objProductoLogica = new ClProductoLogica();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga inicial del GridView
                MtCargarProductos();
            }
        }

        // ==========================================================
        // FLUJO 1: Carga y Edición de Producto (RF-07)
        // ==========================================================

        protected void gridProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProducto")
            {
                int idProductoAEditar = Convert.ToInt32(e.CommandArgument);
                MtCargarProductoEnFormulario(idProductoAEditar);
            }
        }

        private void MtCargarProductoEnFormulario(int idProducto)
        {
            Producto producto = objProductoLogica.ObtenerProductoPorId(idProducto);

            if (producto != null)
            {
                // CRÍTICO: Almacenar el ID en el HiddenField
                hfIdProducto.Value = producto.idProducto.ToString();

                // Rellenar campos
                txtNombre.Text = producto.nombre;
                txtDescripcion.Text = producto.descripcion;
                txtPrecio.Text = producto.precio.ToString();
                txtPrecioProovedor.Text = producto.precioProovedor.ToString();
                txtStock.Text = producto.stock.ToString();

                // Seleccionar Categoría
                try
                {
                    ddlCategoria.SelectedValue = producto.idCategoria.ToString();
                }
                catch
                {
                    // No hace nada si la categoría no existe en el DropDownList
                }

                // Actualizar la interfaz (Modo Edición)
                lblImagenActual.Text = $"Imagen actual: {Path.GetFileName(producto.imgUrl)}";
                btnGuardarProducto.Text = "💾 Actualizar Producto";
                lblTituloFormulario.Text = $"✏️ Editar Producto ID: {idProducto}";
                btnCancelarEdicion.Visible = true;

                MostrarAlerta("Modo Edición", $"Datos de '{producto.nombre}' cargados.", "info");
            }
            else
            {
                MostrarAlerta("Error", "Producto no encontrado.", "error");
            }
        }

        // ==========================================================
        // FLUJO 2: Guardado (Creación o Actualización)
        // ==========================================================

        protected void BtnGuardarProducto_Click(object sender, EventArgs e)
        {
            // 1. Determinar el modo (Edición si hfIdProducto tiene valor)
            bool esEdicion = !string.IsNullOrEmpty(hfIdProducto.Value);
            int idProducto = esEdicion ? Convert.ToInt32(hfIdProducto.Value) : 0;
            string urlImagenActual = string.Empty;

            if (esEdicion)
            {
                // Obtener la URL de imagen actual si no se sube una nueva
                Producto productoExistente = objProductoLogica.ObtenerProductoPorId(idProducto);
                if (productoExistente != null)
                {
                    urlImagenActual = productoExistente.imgUrl;
                }
            }


            // 2. Crear el objeto Producto con los datos del formulario
            Producto objProducto = new Producto()
            {
                idProducto = idProducto,
                nombre = txtNombre.Text,
                descripcion = txtDescripcion.Text,
                // Manejo de conversión de tipos de texto a numérico/decimal
                precio = Convert.ToInt32(txtPrecio.Text),
                precioProovedor = Convert.ToInt32(txtPrecioProovedor.Text),
                stock = Convert.ToInt32(txtStock.Text),
                idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue),
                imgUrl = urlImagenActual
            };

            // 3. Manejo de Subida de Imagen
            if (FileInput.HasFile)
            {
                // Guarda el archivo y actualiza la URL en el objeto
                objProducto.imgUrl = MtGuardarImagen(FileInput);
            }

            // 4. Llamada a la Capa de Lógica
            string mensajeError = string.Empty;
            bool resultado;

            if (esEdicion)
            {
                // Llama al método de ACTUALIZACIÓN (RF-07)
                resultado = objProductoLogica.MtActualizarProducto(objProducto, out mensajeError);
            }
            else
            {
                // Llama al método de REGISTRO
                resultado = objProductoLogica.MtRegistrarProducto(objProducto, out mensajeError);
            }

            // 5. Mostrar Resultados
            if (resultado)
            {
                MostrarAlerta("Éxito", $"Producto {(esEdicion ? "actualizado" : "registrado")} correctamente.", "success");
                MtLimpiarFormulario();
                MtCargarProductos(); // Recargar el GridView
            }
            else
            {
                MostrarAlerta("Error", mensajeError, "error");
            }
        }

        protected void BtnCancelarEdicion_Click(object sender, EventArgs e)
        {
            MtLimpiarFormulario();
            MostrarAlerta("Modo Creación", "Formulario limpiado, listo para nuevo registro.", "info");
        }

        // ==========================================================
        // MÉTODOS DE SOPORTE
        // ==========================================================

        private void MtCargarProductos()
        {
            // Usa el método de la capa de lógica para llenar el GridView
            gridProductos.DataSource = objProductoLogica.MtObtenerProductos();
            gridProductos.DataBind();
        }

        private void MtLimpiarFormulario()
        {
            // CRÍTICO: Limpia el HiddenField para volver a modo "Creación"
            hfIdProducto.Value = string.Empty;

            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtPrecioProovedor.Text = string.Empty;
            txtStock.Text = string.Empty;
            lblImagenActual.Text = string.Empty;

            lblTituloFormulario.Text = "✍️ Registrar Nuevo Producto";
            btnGuardarProducto.Text = "➕ Registrar Producto";
            btnCancelarEdicion.Visible = false;
        }

        private string MtGuardarImagen(FileUpload fileControl)
        {
            // Implementación simplificada para obtener la ruta
            // DEBES IMPLEMENTAR AQUÍ EL CÓDIGO REAL PARA GUARDAR EL ARCHIVO EN EL SERVIDOR
            string nombreArchivo = Path.GetFileName(fileControl.FileName);
            string rutaRelativa = "~/Imagenes/Productos/" + nombreArchivo;

            // Ejemplo de guardado (descomentar y asegurar que la ruta exista):
            // try
            // {
            //     fileControl.SaveAs(Server.MapPath(rutaRelativa));
            // }
            // catch (Exception ex)
            // {
            //     // Manejo de error de subida
            // }

            return rutaRelativa;
        }

        // ==========================================================
        // MÉTODO MODIFICADO: MostrarAlerta con SweetAlert2
        // ==========================================================
        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // 1. Mapear el tipo a los iconos de SweetAlert2
            string icon;
            switch (tipo.ToLower())
            {
                case "success":
                    icon = "success";
                    break;
                case "error":
                    icon = "error";
                    break;
                case "info":
                    icon = "info";
                    break;
                default:
                    icon = "warning";
                    break;
            }

            // 2. Crear el script de SweetAlert2
            // Usamos .Replace para escapar las comillas simples que puedan estar en el mensaje
            string script = $@"
                Swal.fire({{
                    title: '{titulo}',
                    text: '{mensaje.Replace("'", "\\'")}', 
                    icon: '{icon}',
                    confirmButtonText: 'Aceptar'
                }});
            ";

            // 3. Registrar el script usando ScriptManager.RegisterStartupScript
            // El último parámetro 'false' indica que el script debe ejecutarse después del postback
            ScriptManager.RegisterStartupScript(this, GetType(), "SwalAlert", script, true);
        }
    }
}