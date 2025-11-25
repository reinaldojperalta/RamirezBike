<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="AppRamirezBike.Vista.admin.Usuarios" %>

<%-- 
    CORRECCIÓN 1: Solo hay UN bloque <asp:Content>.
    Todo el HTML y el JavaScript van dentro de este único bloque.
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <!-- ===================================================== -->
    <!-- CONTENIDO PRINCIPAL DE LA PÁGINA (HTML) -->
    <!-- ===================================================== -->

    <!-- Botón para crear un nuevo usuario -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Usuarios</h3>
        <!-- <button class="btn btn-primary" data-bs-toggle="collapse" data-bs-target="#collapseFormulario" aria-expanded="false" aria-controls="collapseFormulario">
            <i class="bi bi-plus-circle"></i> Nuevo Usuario
        </button>-->
    </div>

    <!-- Contenedor para la tabla de usuarios -->
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
                <tbody>
                    <!-- Las filas se cargarán dinámicamente con JavaScript y DataTables -->
                </tbody>
            </table>
        </div>
    </div>

    <!-- ===================================================== -->
    <!-- FORMULARIO COLAPSABLE PARA CREAR/EDITAR -->
    <!-- ===================================================== -->
    <div class="collapse mt-4" id="collapseFormulario">
        <div class="card">
            <div class="card-header">
                <h5 id="tituloFormulario">Nuevo Usuario</h5>
            </div>
            <div class="card-body">
                <%-- 
                    CORRECCIÓN 2: Se eliminan las etiquetas <form>.
                    El contenido ya está dentro del <form> de la página maestra.
                --%>
                <div id="formUsuarios" class="row g-3 needs-validation" novalidate>
                    <!-- Campo oculto para el ID del usuario (crucial para editar) -->
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
                            <span class="input-group-text" id="inputGroupPrepend">@</span>
                            <input type="email" class="form-control" id="validationEmail" aria-describedby="inputGroupPrepend" required>
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
                            <!-- Las opciones se cargarán dinámicamente con JavaScript -->
                        </select>
                        <div class="invalid-feedback">Por favor, selecciona un rol.</div>
                    </div>

                
                    <div class="col-12">
                        <button id="btnSubmit" class="btn btn-success" type="button">
                            <i class="bi bi-check-circle"></i> Guardar
                        </button>
                           <button id="btnCancelar" class="btn btn-secondary" type="button">
                                <i class="bi bi-x-circle"></i> Cancelar
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- ===================================================== -->
    <!-- BLOQUE DE SCRIPTS (JavaScript) -->
    <!-- ===================================================== -->
<script type="text/javascript">
    $(document).ready(function () {
        let tabla; // Variable global para la instancia de DataTables

        // --- 1. INICIALIZAR DATATABLES ---
        function inicializarDataTable() {
            tabla = $('#tablaUsuarios').DataTable({
                // Llamada AJAX al método web C#
                ajax: {
                    url: 'Usuarios.aspx/ListarUsuarios', // Ruta al método web
                    method: 'POST', // Método HTTP
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    dataSrc: function (json) {
                        // La respuesta de ASP.NET WebMethods viene envuelta en un objeto 'd'
                        return json.d;
                    }
                },
                columns: [
                    { data: 'idUsuario' },
                    { data: 'nombreCompleto' },
                    { data: 'email' },
                    { data: 'telefono' },
                    { data: 'nombreRol' },
                    {
                        // Columna para los botones de acción
                        data: null,
                        render: function (data, type, row) {
                            // 'data' es el objeto completo de la fila (el usuario)
                            return `
                                    <button class="btn btn-sm btn-warning btn-editar" data-id="${data.idUsuario}">
                                        <i class="bi bi-pencil"></i>
                                    </button>
                                    <button class="btn btn-sm btn-danger btn-inhabilitar" data-id="${data.idUsuario}">
                                        <i class="bi bi-x-circle"></i>
                                    </button>
                                `;
                        }
                    }
                ],
                // Configuración de idioma y botones de exportación
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json'
                },
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ]
            });
        }

        // --- 2. FUNCIÓN PARA CARGAR ROLES EN EL DROPDOWN ---
        function cargarRoles() {
            $.ajax({
                url: 'Usuarios.aspx/ListarRoles', // Llama al nuevo WebMethod
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    const roles = response.d;
                    const dropdown = $('#validationRol');
                    dropdown.empty(); // Limpia opciones previas
                    dropdown.append('<option value="" disabled selected>Selecciona un rol...</option>'); // Opción por defecto

                    roles.forEach(function (rol) {
                        dropdown.append(`<option value="${rol.idRol}">${rol.nombre}</option>`);
                    });
                },
                error: function (err) {
                    alert('Error al cargar los roles: ' + err.responseText);
                }
            });
        }

        // Inicializamos todo cuando la página está lista
        inicializarDataTable();
        cargarRoles();

        // --- 3. MANEJO DE EVENTOS ---

        // Evento para el botón GUARDAR (crear/editar)
        $('#btnSubmit').on('click', function (e) {
            e.preventDefault();

            // Validación manual de los campos requeridos
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
                alert('Por favor, completa todos los campos requeridos.');
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

            // Llamada AJAX al método web para actualizar
            $.ajax({
                url: 'Usuarios.aspx/ActualizarUsuario',
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ oUsuario: usuario }),
                success: function (response) {
                    alert(response.d);
                    $('#collapseFormulario').collapse('hide');
                    setTimeout(function () {
                        limpiarFormulario();
                        tabla.ajax.reload(false); // 'false' mantiene la página actual
                    }, 400);
                },
                error: function (err) {
                    alert('Error en la llamada AJAX: ' + err.responseText);
                }
            });
        });

        // Evento para el botón CANCELAR
        $('#btnCancelar').on('click', function () {
            limpiarFormulario();
            $('#collapseFormulario').collapse('hide');
        });

        // Evento para el botón de EDITAR (usando delegación de eventos)
        $('#tablaUsuarios tbody').on('click', '.btn-editar', function () {
            const id = $(this).data('id');

            // Llamada AJAX para obtener los datos del usuario
            $.ajax({
                url: 'Usuarios.aspx/ObtenerUsuarioPorId',
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ idUsuario: id }),
                success: function (response) {
                    const usuario = response.d;
                    $('#tituloFormulario').text('Editar Usuario');
                    $('#idUsuario').val(usuario.idUsuario);
                    $('#validationNombre').val(usuario.nombre);
                    $('#validationApellido').val(usuario.apellido);
                    $('#validationEmail').val(usuario.email);
                    $('#validationTelefono').val(usuario.telefono);
                    $('#validationRol').val(usuario.idRol);

                    // Solo mostramos el formulario si no está visible
                    if (!$('#collapseFormulario').hasClass('show')) {
                        $('#collapseFormulario').collapse('show');
                    }
                },
                error: function (err) {
                    alert('Error al obtener el usuario: ' + err.responseText);
                }
            });
        });

        // Evento para el botón de INHABILITAR (usando delegación de eventos)
        $('#tablaUsuarios tbody').on('click', '.btn-inhabilitar', function () {
            const id = $(this).data('id');
            if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
                // Llamada AJAX para inhabilitar
                $.ajax({
                    url: 'Usuarios.aspx/InhabilitarUsuario',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ idUsuario: id }),
                    success: function (response) {
                        alert(response.d);
                        tabla.ajax.reload();
                    },
                    error: function (err) {
                        alert('Error al eliminar el usuario: ' + err.responseText);
                    }
                });
            }
        });

        // --- 4. FUNCIONES AUXILIARES ---
        function limpiarFormulario() {
            // Limpiamos cada campo manualmente
            $('#formUsuarios input, #formUsuarios select').each(function () {
                $(this).val('');
            });
            // Reseteamos los campos a su estado por defecto
            $('#idUsuario').val(0);
            $('#tituloFormulario').text('Nuevo Usuario');
            // Quitamos las clases de validación de Bootstrap
            $('#formUsuarios input, #formUsuarios select').removeClass('is-invalid');
        }
    });
</script>

</asp:Content>