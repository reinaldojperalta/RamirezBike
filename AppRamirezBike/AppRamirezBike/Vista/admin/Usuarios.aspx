<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AppRamirezBike.Vista.Admin.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <!-- Título -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Usuarios</h3>
    </div>

    <!-- Tabla -->
    <div class="card">
        <div class="card-body">
            <table id="tablaUsuarios" class="table table-striped table-hover" style="width:100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre Completo</th>
                        <th>Email</th>
                        <th>Teléfono</th>
                        <th>Rol</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>

    <!-- Formulario colapsable -->
    <div class="collapse mt-4" id="collapseFormulario">
        <div class="card">
            <div class="card-header bg-primary text-white">
                <h5 id="tituloFormulario" class="mb-0">Nuevo Usuario</h5>
            </div>
            <div class="card-body">
                <div id="formUsuarios" class="row g-3 needs-validation" novalidate>
                    <input type="hidden" id="idUsuario" value="0" />
                    <div class="col-md-6">
                        <label for="validationNombre" class="form-label">Nombre:</label>
                        <input type="text" class="form-control" id="validationNombre" required>
                        <div class="invalid-feedback">Por favor, ingresa un nombre.</div>
                    </div>
                    <div class="col-md-6">
                        <label for="validationApellido" class="form-label">Apellido:</label>
                        <input type="text" class="form-control" id="validationApellido" required>
                        <div class="invalid-feedback">Por favor, ingresa un apellido.</div>
                    </div>
                    <div class="col-md-6">
                        <label for="validationEmail" class="form-label">Email:</label>
                        <div class="input-group has-validation">
                            <span class="input-group-text">@</span>
                            <input type="email" class="form-control" id="validationEmail" required>
                            <div class="invalid-feedback">Por favor, ingresa un email válido.</div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label for="validationTelefono" class="form-label">Teléfono:</label>
                        <input type="text" class="form-control" id="validationTelefono" required>
                        <div class="invalid-feedback">Por favor, ingresa un teléfono.</div>
                    </div>
                    <div class="col-md-6">
                        <label for="validationRol" class="form-label">Rol:</label>
                        <select class="form-select" id="validationRol" required>
                            <option value="" disabled selected>Selecciona un rol...</option>
                        </select>
                        <div class="invalid-feedback">Por favor, selecciona un rol.</div>
                    </div>
                    <div class="col-12 text-end mt-3">
                        <button id="btnCancelar" class="btn btn-secondary me-2" type="button">Cancelar</button>
                        <button id="btnSubmit" class="btn btn-success" type="button">
                            <i class="bi bi-save"></i> Guardar
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            let tabla;

            function inicializarDataTable() {
                tabla = $('#tablaUsuarios').DataTable({
                    ajax: {
                        url: 'Usuarios.aspx/ListarUsuarios',
                        method: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        dataSrc: function (json) { return json.d; }
                    },
                    columns: [
                        { data: 'idUsuario' },
                        { data: 'nombreCompleto' },
                        { data: 'email' },
                        { data: 'telefono' },
                        { data: 'nombreRol' },
                        {
                            data: null,
                            render: function (data) {
                                return `
                                    <button class="btn btn-sm btn-warning btn-editar" data-id="${data.idUsuario}">
                                        <i class="bi bi-pencil"></i>
                                    </button>
                                    <button class="btn btn-sm btn-danger btn-inhabilitar" data-id="${data.idUsuario}">
                                        <i class="bi bi-x-circle"></i>
                                    </button>`;
                            }
                        }
                    ],
                    language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                    dom: 'Bfrtip',
                    buttons: ['copy', 'csv', 'excel', 'pdf', 'print']
                });
            }

            function cargarRoles() {
                $.ajax({
                    url: 'Usuarios.aspx/ListarRoles',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        const dropdown = $('#validationRol');
                        dropdown.empty();
                        dropdown.append('<option value="" disabled selected>Selecciona un rol...</option>');
                        response.d.forEach(rol => {
                            dropdown.append(`<option value="${rol.idRol}">${rol.nombre}</option>`);
                        });
                    },
                    error: function () {
                        Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudieron cargar los roles', confirmButtonColor: '#dc3545' });
                    }
                });
            }

            inicializarDataTable();
            cargarRoles();

            // BOTÓN GUARDAR
            $('#btnSubmit').on('click', function () {
                let isValid = true;
                $('#formUsuarios input[required], #formUsuarios select[required]').each(function () {
                    if ($(this).val() === '') {
                        $(this).addClass('is-invalid');
                        isValid = false;
                    } else {
                        $(this).removeClass('is-invalid');
                    }
                });

                if (!isValid) {
                    Swal.fire({ icon: 'warning', title: '¡Faltan datos!', text: 'Completa todos los campos requeridos', confirmButtonColor: '#198754' });
                    return;
                }

                const usuario = {
                    idUsuario: parseInt($('#idUsuario').val()),
                    nombre: $('#validationNombre').val(),
                    apellido: $('#validationApellido').val(),
                    email: $('#validationEmail').val(),
                    telefono: $('#validationTelefono').val(),
                    idRol: parseInt($('#validationRol').val())
                };

                $.ajax({
                    url: 'Usuarios.aspx/ActualizarUsuario',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ oUsuario: usuario }),
                    success: function (response) {
                        Swal.fire({ icon: 'success', title: '¡Perfecto!', text: response.d, confirmButtonColor: '#198754' }).then(() => {
                            $('#collapseFormulario').collapse('hide');
                            limpiarFormulario();
                            tabla.ajax.reload(false);
                        });
                    },
                    error: function (err) {
                        Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudo guardar el usuario', confirmButtonColor: '#dc3545' });
                    }
                });
            });

            // BOTÓN CANCELAR
            $('#btnCancelar').on('click', function () {
                $('#collapseFormulario').collapse('hide');
                limpiarFormulario();
            });

            // BOTÓN EDITAR
            $('#tablaUsuarios tbody').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                $.ajax({
                    url: 'Usuarios.aspx/ObtenerUsuarioPorId',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ idUsuario: id }),
                    success: function (response) {
                        const u = response.d;
                        $('#tituloFormulario').text('Editar Usuario');
                        $('#idUsuario').val(u.idUsuario);
                        $('#validationNombre').val(u.nombre);
                        $('#validationApellido').val(u.apellido);
                        $('#validationEmail').val(u.email);
                        $('#validationTelefono').val(u.telefono);
                        $('#validationRol').val(u.idRol);
                        $('#collapseFormulario').collapse('show');
                    },
                    error: function () {
                        Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudo cargar el usuario', confirmButtonColor: '#dc3545' });
                    }
                });
            });

            // BOTÓN INHABILITAR (ELIMINAR)
            $('#tablaUsuarios tbody').on('click', '.btn-inhabilitar', function () {
                const id = $(this).data('id');
                Swal.fire({
                    title: '¿Eliminar usuario?',
                    text: 'Esta acción no se puede deshacer',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#dc3545',
                    cancelButtonColor: '#6c757d',
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then(result => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: 'Usuarios.aspx/InhabilitarUsuario',
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ idUsuario: id }),
                            success: function (response) {
                                Swal.fire({ icon: 'success', title: '¡Eliminado!', text: response.d, confirmButtonColor: '#198754' });
                                tabla.ajax.reload();
                            },
                            error: function () {
                                Swal.fire({ icon: 'error', title: 'Error', text: 'No se pudo eliminar el usuario', confirmButtonColor: '#dc3545' });
                            }
                        });
                    }
                });
            });

            function limpiarFormulario() {
                $('#formUsuarios')[0].reset();
                $('#idUsuario').val(0);
                $('#tituloFormulario').text('Nuevo Usuario');
                $('#formUsuarios input, #formUsuarios select').removeClass('is-invalid');
            }
        });
    </script>
</asp:Content>