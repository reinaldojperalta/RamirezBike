<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="AppRamirezBike.Vista.Admin.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Productos</h3>
        <button class="btn btn-primary" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFormulario" aria-expanded="false" aria-controls="collapseFormulario" onclick="limpiarFormulario()">
            <i class="bi bi-plus-circle"></i> Nuevo Producto
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
                <tbody></tbody>
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
                    <div class="col-md-6">
                        <label for="valNombre" class="form-label">Nombre del Producto:</label>
                        <input type="text" class="form-control" id="valNombre" required>
                    </div>
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
                    <div class="col-12">
                        <label for="valDescripcion" class="form-label">Descripción:</label>
                        <textarea class="form-control" id="valDescripcion" rows="3"></textarea>
                    </div>
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
                    <div class="col-12 text-end mt-3">
                        <button id="btnCancelar" class="btn btn-secondary me-2" type="button">Cancelar</button>
                        <button id="btnSubmit" class="btn btn-success" type="button">
                            <i class="bi bi-save"></i> Guardar Producto
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        const pageUrl = "Productos.aspx";
        let tabla;
        const imgBasePath = '<%= ResolveUrl("~/Vista/img/") %>';

        $(document).ready(function () {
            function inicializarDataTable() {
                tabla = $('#tablaProductos').DataTable({
                    ajax: {
                        url: pageUrl + '/ListarTodosProductos',
                        method: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        dataSrc: function (json) { return json.d; }
                    },
                    columns: [
                        { data: 'idProducto' },
                        {
                            data: 'imgUrl',
                            render: data => data ? `<img src="${imgBasePath}${data}" style="max-height: 50px;">` : 'N/A'
                        },
                        { data: 'nombre' },
                        { data: 'descripcion' },
                        { data: 'precio' },
                        { data: 'stock' },
                        {
                            data: 'idProducto',
                            orderable: false,
                            render: data => `<button class="btn btn-sm btn-warning btn-editar" data-id="${data}"><i class="bi bi-pencil"></i></button>
                                             <button class="btn btn-sm btn-danger btn-eliminar" data-id="${data}"><i class="bi bi-trash"></i></button>`
                        }
                    ],
                    language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                    dom: 'Bfrtip',
                    buttons: ['copy', 'excel', 'pdf']
                });
            }

            function cargarCategorias() {
                $.ajax({
                    url: pageUrl + '/ListarCategorias',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        const select = $('#valCategoria');
                        select.find('option:not(:first)').remove();
                        $.each(response.d, function (i, cat) {
                            select.append(`<option value="${cat.idCategoria}">${cat.nombre}</option>`);
                        });
                    }
                });
            }

            function subirImagen(file) {
                if (!file) return Promise.resolve(null);
                const formData = new FormData();
                formData.append('file', file);
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'SubirImagen.aspx',
                        method: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        cache: false,
                        success: function (response) {
                            try {
                                // Intentamos parsear como JSON
                                let json = typeof response === 'string' ? JSON.parse(response) : response;
                                if (json.success) {
                                    resolve(json.filename);
                                } else {
                                    reject(json.error || "Error desconocido");
                                }
                            } catch (e) {
                                // Si no es JSON, es texto plano (tu SubirImagen.aspx devuelve solo el nombre)
                                let nombre = response.trim().replace(/^"|"$/g, '');
                                resolve(nombre);
                            }
                        },
                        error: function (xhr) {
                            reject("Error de conexión: " + xhr.statusText);
                        }
                    });
                });
            }
            

            inicializarDataTable();
            cargarCategorias();

            // ==================== BOTÓN GUARDAR ====================
            $('#btnSubmit').on('click', function () {
                // Validaciones
                if (!$('#valNombre').val() || !$('#valPrecio').val() || !$('#valStock').val() || !$('#valCategoria').val()) {
                    Swal.fire({ icon: 'warning', title: '¡Faltan datos!', text: 'Completa todos los campos obligatorios', confirmButtonColor: '#198754' });
                    return;
                }

                const file = document.getElementById('fileImagen').files[0];
                if ($('#idProducto').val() === '0' && !file) {
                    Swal.fire({ icon: 'warning', title: '¡Falta la imagen!', text: 'Selecciona una imagen para el producto nuevo', confirmButtonColor: '#198754' });
                    return;
                }

                // Mostrar cargando
                Swal.fire({
                    title: 'Guardando producto...',
                    allowOutsideClick: false,
                    didOpen: () => { Swal.showLoading(); }
                });

                subirImagen(file)
                    .then(nombreNuevo => {
                        const nombreFinal = nombreNuevo || $('#valImgUrl').val() || '';

                        const producto = {
                            idProducto: parseInt($('#idProducto').val()),
                            nombre: $('#valNombre').val(),
                            descripcion: $('#valDescripcion').val() || '',
                            precio: parseFloat($('#valPrecio').val()),
                            stock: parseInt($('#valStock').val()),
                            imgUrl: nombreFinal,
                            idCategoria: parseInt($('#valCategoria').val())
                        };

                        return $.ajax({
                            url: pageUrl + '/GuardarProducto',
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ objProducto: producto })
                        });
                    })
                    .then(res => {
                        Swal.fire({ icon: 'success', title: '¡Perfecto!', text: res.d || 'Producto guardado correctamente', confirmButtonColor: '#198754' }).then(() => {
                            $('#collapseFormulario').collapse('hide');
                            tabla.ajax.reload();
                            limpiarFormulario();
                        });
                    })
                    .catch(err => {
                        console.error("Error:", err);
                        Swal.fire({ icon: 'error', title: 'Error', text: err || 'No se pudo completar la operación', confirmButtonColor: '#dc3545' });
                    });
            });

            // ==================== BOTÓN EDITAR ====================
            $('#tablaProductos tbody').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                $.ajax({
                    url: pageUrl + '/ObtenerProducto',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ idProducto: id }),
                    success: function (res) {
                        const p = res.d;
                        $('#idProducto').val(p.idProducto);
                        $('#valNombre').val(p.nombre);
                        $('#valDescripcion').val(p.descripcion);
                        $('#valPrecio').val(p.precio);
                        $('#valStock').val(p.stock);
                        $('#valImgUrl').val(p.imgUrl);
                        $('#imgNombreActual').text('Imagen actual: ' + (p.imgUrl || 'Ninguna'));
                        $('#valCategoria').val(p.idCategoria);
                        $('#tituloFormulario').text("Editar Producto");
                        $('#collapseFormulario').collapse('show');
                    }
                });
            });

            // ==================== BOTÓN ELIMINAR ====================
            $('#tablaProductos tbody').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Swal.fire({
                    title: '¿Eliminar producto?',
                    text: "Esta acción no se puede deshacer",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#dc3545',
                    cancelButtonColor: '#6c757d',
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then(result => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: pageUrl + '/EliminarProducto',
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ idProducto: id }),
                            success: function (res) {
                                Swal.fire({ icon: 'success', title: 'Eliminado', text: res.d, confirmButtonColor: '#198754' });
                                tabla.ajax.reload();
                            }
                        });
                    }
                });
            });

            // ==================== CANCELAR ====================
            $('#btnCancelar').on('click', function () {
                $('#collapseFormulario').collapse('hide');
                limpiarFormulario();
            });

            function limpiarFormulario() {
                $('#formProductos')[0].reset();
                $('#idProducto').val(0);
                $('#tituloFormulario').text('Nuevo Producto');
                $('#imgNombreActual').text('');
                $('#valImgUrl').val('');
            }
        });
    </script>
</asp:Content>