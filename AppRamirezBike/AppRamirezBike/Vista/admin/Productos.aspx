<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="AppRamirezBike.Vista.Admin.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Productos</h3>
        <button class="btn btn-primary" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFormulario" aria-expanded="false" aria-controls="collapseFormulario" onclick="limpiarFormulario()">
            <i class="bi bi-plus-circle"></i>Nuevo Producto
        </button>
    </div>

    <div class="card">
        <div class="card-body">
            <table id="tablaProductos" class="table table-striped table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Imagen</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Precio</th>
                        <th>Stock</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

    <div class="collapse mt-4" id="collapseFormulario">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 id="tituloFormulario" class="mb-0">Nuevo Producto</h5>
            </div>
            <div class="card-body">
                <div class="row g-3 needs-validation" id="formProductos" novalidate>
                    <input type="hidden" id="idProducto" value="0" />

                    <!-- NOMBRE -->
                    <div class="col-md-6">
                        <label for="valNombre" class="form-label">Nombre del Producto:</label>
                        <input type="text" class="form-control" id="valNombre" required>
                    </div>

                    <!-- PRECIO Y STOCK EN LA MISMA FILA -->
                    <div class="col-md-3">
                        <label for="valPrecio" class="form-label">Precio:</label>
                        <div class="input-group">
                            <span class="input-group-text">$</span>
                            <input type="number" class="form-control" id="valPrecio" required min="0">
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label for="valStock" class="form-label">Stock:</label>
                        <input type="number" class="form-control" id="valStock" required min="0">
                    </div>

                    <!-- DESCRIPCIÓN -->
                    <div class="col-12">
                        <label for="valDescripcion" class="form-label">Descripción:</label>
                        <textarea class="form-control" id="valDescripcion" rows="3"></textarea>
                    </div>

                    <!-- IMAGEN Y CATEGORÍA EN LA MISMA FILA -->
                    <div class="col-md-6">
                        <label for="fileImagen" class="form-label">Imagen del Producto:</label>
                        <input type="file" class="form-control" id="fileImagen" accept="image/*">
                        <input type="hidden" id="valImgUrl" value="">
                        <small class="text-muted" id="imgNombreActual"></small>
                    </div>
                    <div class="col-md-6">
                        <label for="valCategoria" class="form-label">Categoría:</label>
                        <select class="form-select" id="valCategoria" required>
                            <option value="" disabled selected>Selecciona una categoría...</option>
                        </select>
                    </div>

                    <!-- BOTONES -->
                    <div class="col-12 text-end mt-3">
                        <button id="btnCancelar" class="btn btn-secondary me-2" type="button">Cancelar</button>
                        <button id="btnSubmit" class="btn btn-success" type="button">
                            <i class="bi bi-save"></i>Guardar Producto
       
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        // Variable global para la URL de la página
        const pageUrl = "Productos.aspx";
        let tabla;

        // 💡 RUTA ABSOLUTA CORREGIDA: Apunta a ~/Vista/img/
        const imgBasePath = '<%= ResolveUrl("~/Vista/img/") %>';

        $(document).ready(function () {

            // 1. INICIALIZAR DATATABLE
            function inicializarDataTable() {
                tabla = $('#tablaProductos').DataTable({
                    // ... (resto del código de inicialización de DataTables, se mantiene) ...
                    ajax: {
                        url: pageUrl + '/ListarTodosProductos',
                        method: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        dataSrc: function (json) { return json.d; },
                        error: function (xhr, error, thrown) {
                            console.error("Error AJAX de DataTables:", thrown);
                            alert("Error al cargar la tabla. Verifique el WebMethod.");
                        }
                    },
                    columns: [
                        { data: 'idProducto' },
                        {
                            data: 'imgUrl',
                            render: function (data) {
                                // USANDO LA RUTA ABSOLUTA CORREGIDA
                                return data ? `<img src="${imgBasePath}${data}" style="max-height: 50px;">` : 'N/A';
                            }
                        },
                        { data: 'nombre' },
                        { data: 'descripcion' },
                        { data: 'precio' },
                        { data: 'stock' },
                        {
                            data: 'idProducto',
                            orderable: false,
                            render: function (data, type, row) {
                                return `<button class="btn btn-sm btn-warning btn-editar" data-id="${data}"><i class="bi bi-pencil"></i></button>
                                        <button class="btn btn-sm btn-danger btn-eliminar" data-id="${data}"><i class="bi bi-trash"></i></button>`;
                            }
                        }
                    ],
                    // 💡 RUTA DEL IDIOMA CORREGIDA (usando HTTPS completo)
                    language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                    dom: 'Bfrtip',
                    buttons: ['copy', 'excel', 'pdf']
                });
            }


            // 2. FUNCIÓN PARA CARGAR EL DROPDOWN DE CATEGORÍAS
            function cargarCategorias() {
                // ... (código para cargar categorías, se mantiene) ...
                $.ajax({
                    url: pageUrl + '/ListarCategorias',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        const selectCategoria = $('#valCategoria');
                        selectCategoria.find('option:not(:first)').remove();

                        $.each(response.d, function (i, categoria) {
                            selectCategoria.append($('<option>', {
                                value: categoria.idCategoria,
                                text: categoria.nombre
                            }));
                        });
                    },
                    error: function (err) {
                        console.error("Error al cargar categorías:", err.responseText);
                    }
                });
            }

            // 7. FUNCIÓN PARA SUBIR LA IMAGEN AL SERVIDOR (NUEVA)
            function subirImagen(file) {
                if (!file) return Promise.resolve(null);

                const formData = new FormData();
                formData.append('file', file);

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'SubirImagen.aspx',  // o '../SubirImagen.aspx' si está fuera
                        method: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (response) {
                            // response es texto, lo convertimos a objeto
                            let resultado;
                            try {
                                resultado = typeof response === "string" ? JSON.parse(response) : response;
                            } catch (e) {
                                resultado = { success: true, filename: response }; // fallback
                            }

                            if (resultado.success) {
                                resolve(resultado.filename);
                            } else {
                                alert("Error al subir imagen: " + (resultado.error || "desconocido"));
                                reject(resultado.error);
                            }
                        },
                        error: function () {
                            alert("Error de conexión al subir imagen");
                            reject("Error de conexión");
                        }
                    });
                });
            }

            // Inicialización al cargar el DOM
            inicializarDataTable();
            cargarCategorias();

            // 3. GUARDAR/ACTUALIZAR PRODUCTO (CRUD - C/U) - FLUJO MODIFICADO
            $('#btnSubmit').on('click', function (e) {
                e.preventDefault();

                // 1. Validación de campos obligatorios
                if ($('#valNombre').val() === '' || $('#valPrecio').val() === '' || $('#valStock').val() === '' || $('#valCategoria').val() === null) {
                    alert("Completa los campos obligatorios");
                    return;
                }

                const fileInput = document.getElementById('fileImagen');
                const file = fileInput.files[0];

                // Si es un producto nuevo y no hay archivo, detener la acción.
                if ($('#idProducto').val() === '0' && !file) {
                    alert("Debes seleccionar una imagen para un producto nuevo.");
                    return;
                }

                let nombreImagenActual = $('#valImgUrl').val(); // Nombre actual si estamos editando

                // 2. Ejecuta la subida de la imagen
                subirImagen(file)
                    .then(nombreNuevoArchivo => {
                        // Si se subió un archivo, usamos el nuevo nombre. Si no, usamos el nombre actual (si estamos editando)
                        const nombreFinalImagen = nombreNuevoArchivo || nombreImagenActual;

                        // 3. Construir objeto Producto con el nombre de imagen final
                        const producto = {
                            idProducto: parseInt($('#idProducto').val()),
                            nombre: $('#valNombre').val(),
                            descripcion: $('#valDescripcion').val(),
                            precio: parseFloat($('#valPrecio').val()),
                            stock: parseInt($('#valStock').val()),
                            imgUrl: nombreFinalImagen, // ¡USAMOS EL NOMBRE FINAL!
                            idCategoria: parseInt($('#valCategoria').val())
                        };

                        // 4. Llamar al WebMethod C# para guardar los datos en la BD
                        return $.ajax({
                            url: pageUrl + '/GuardarProducto',
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ objProducto: producto }),
                        });
                    })
                    .then(response => {
                        // 5. Éxito al guardar los datos
                        alert(response.d);
                        $('#collapseFormulario').collapse('hide');
                        tabla.ajax.reload();
                        limpiarFormulario();
                    })
                    .catch(error => {
                        // Manejo de error de subida o guardado en BD
                        alert("Ocurrió un error al procesar la solicitud.");
                        console.error("Error en flujo de guardado:", error);
                    });
            });

            // 4. EDITAR (Cargar datos en el formulario) - ACTUALIZADO
            $('#tablaProductos tbody').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                $.ajax({
                    url: pageUrl + '/ObtenerProducto',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ idProducto: id }),
                    success: function (response) {
                        const p = response.d;

                        $('#idProducto').val(p.idProducto);
                        $('#valNombre').val(p.nombre);
                        $('#valDescripcion').val(p.descripcion);
                        $('#valPrecio').val(p.precio);
                        $('#valStock').val(p.stock);

                        // 🟢 NUEVO: Guarda el nombre de archivo actual en el input hidden
                        $('#valImgUrl').val(p.imgUrl);
                        $('#imgNombreActual').text('Imagen actual: ' + (p.imgUrl || 'No asignada'));

                        $('#valCategoria').val(p.idCategoria);

                        $('#tituloFormulario').text("Editar Producto");
                        $('#collapseFormulario').collapse('show');
                    },
                    error: function (err) { alert("Error al cargar producto: " + err.responseText); }
                });
            });

            // 5. ELIMINAR (CRUD - D)
            $('#tablaProductos tbody').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                if (confirm('¿Deseas eliminar este producto?')) {
                    $.ajax({
                        url: pageUrl + '/EliminarProducto',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ idProducto: id }),
                        success: function (response) {
                            alert(response.d);
                            tabla.ajax.reload();
                        },
                        error: function (err) { alert("Error al eliminar: " + err.responseText); }
                    });
                }
            });

            // 6. Cancelar
            $('#btnCancelar').on('click', function () {
                $('#collapseFormulario').collapse('hide');
                limpiarFormulario();
            });
        });

        // Función auxiliar para limpiar el formulario y resetear el título (ACTUALIZADO)
        function limpiarFormulario() {
            $('#formProductos input, #formProductos textarea, #formProductos select').val('');
            $('#idProducto').val(0);
            $('#tituloFormulario').text('Nuevo Producto');
            $('#fileImagen').val(''); // Limpia el control de subida de archivo
            $('#imgNombreActual').text(''); // Limpia el nombre de imagen actual
            // Asegúrate de resetear la selección de categoría si es necesario
            $('#valCategoria').val('');
        }
    </script>

</asp:Content>
