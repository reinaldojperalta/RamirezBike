<%@ Page Title="Venta Rápida" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master"
    AutoEventWireup="true" CodeBehind="VentaRapida.aspx.cs"
    Inherits="AppRamirezBike.Vista.VentaRapida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <!-- SweetAlert2 -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Bootstrap JS (NECESARIO para el modal) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>


<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentBody1" runat="server">

    <!-- ScriptManager NECESARIO para llamadas AJAX en WebForms -->
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>


    <div class="container mt-5">
        <h2 class="mb-4">Registro de Venta Rápida (RF-09)</h2>

        <div class="row">
            <div class="col-md-12">
                <div class="card p-4 shadow">

                    <h4 class="mb-3">Productos en el Carrito</h4>

                    <button class="btn btn-primary btn-sm mb-3" type="button" onclick="anadirItemALista(1, 'Manillar Carbono PRO', 350.00, 1)">Añadir Manillar (ID 1)</button>
                    <button class="btn btn-warning btn-sm mb-3" type="button" onclick="anadirItemALista(2, 'Pedal Clip', 80.00, 2)">Añadir Pedales (ID 2)</button>

                    <ul id="listaVenta" class="list-group mb-3"></ul>

                    <div class="d-flex justify-content-end align-items-center my-2 border-top pt-2">
                        <span style="font-size: 1.2em; font-weight: bold;">TOTAL:</span>
                        <span id="totalVenta" style="font-size: 2em; color: #198754; margin-left: 15px;">$0.00</span>
                    </div>

                    <button id="btnFinalizarVenta" class="btn btn-success btn-lg mt-3"
                        disabled onclick="mostrarModalConfirmacion()">
                        <i class="bi bi-cash"></i> Finalizar Venta
                    </button>

                </div>
            </div>
        </div>
    </div>


    <!-- MODAL CONFIRMACIÓN -->
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title" id="modalLabel">⚠️ Confirmar Transacción Final</h5>
                </div>

                <div class="modal-body">
                    ¿Estás seguro de finalizar esta venta?
                    <p class="mt-3">Total Venta: <strong id="modalTotal" style="color: #198754;">$0.00</strong></p>
                    <p class="text-danger small" id="estadoCarga" style="display:none;">Procesando venta, por favor espere...</p>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success" id="btnConfirmarProceso" onclick="procesarVenta()">Confirmar Venta</button>
                </div>

            </div>
        </div>
    </div>


    <script type="text/javascript">

        let listaCarrito = [];

        function actualizarListaUI() {
            const ul = $('#listaVenta');
            ul.empty();
            let total = 0;

            if (listaCarrito.length === 0) {
                ul.append('<li class="list-group-item text-muted text-center">No hay productos añadidos.</li>');
                $('#btnFinalizarVenta').prop('disabled', true);
            } else {
                $('#btnFinalizarVenta').prop('disabled', false);

                listaCarrito.forEach(item => {
                    const subtotal = item.precioVenta * item.cantidad;
                    total += subtotal;

                    ul.append(`
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                ${item.nombre} <br><small>ID: ${item.idProducto} - $${item.precioVenta.toFixed(2)} c/u</small>
                            </div>

                            <div class="d-flex align-items-center">
                                <button class="btn btn-sm btn-outline-secondary me-2" onclick="cambiarCantidad(${item.idProducto}, -1)">-</button>
                                <span>${item.cantidad}</span>
                                <button class="btn btn-sm btn-outline-secondary ms-2" onclick="cambiarCantidad(${item.idProducto}, 1)">+</button>
                                <button class="btn btn-sm btn-danger ms-3" onclick="eliminarItem(${item.idProducto})">X</button>
                            </div>

                            <strong>$${subtotal.toFixed(2)}</strong>
                        </li>
                    `);
                });
            }

            $('#totalVenta').text(`$${total.toFixed(2)}`);
            $('#modalTotal').text(`$${total.toFixed(2)}`);
        }


        function anadirItemALista(id, nombre, precio, cantidadInicial = 1) {
            const itemExistente = listaCarrito.find(i => i.idProducto === id);

            if (itemExistente) {
                itemExistente.cantidad += cantidadInicial;
            } else {
                listaCarrito.push({ idProducto: id, nombre, precioVenta: precio, cantidad: cantidadInicial });
            }

            actualizarListaUI();
        }

        function cambiarCantidad(id, delta) {
            const item = listaCarrito.find(i => i.idProducto === id);

            if (item) {
                item.cantidad += delta;
                if (item.cantidad <= 0) eliminarItem(id);
                actualizarListaUI();
            }
        }

        function eliminarItem(id) {
            listaCarrito = listaCarrito.filter(i => i.idProducto !== id);
            actualizarListaUI();
        }

        function mostrarModalConfirmacion() {
            if (listaCarrito.length > 0) {
                new bootstrap.Modal(document.getElementById('modalConfirmacion')).show();
            }
        }

        function procesarVenta() {

            const $btn = $('#btnConfirmarProceso');
            const $estadoCarga = $('#estadoCarga');

            $btn.prop('disabled', true).text('Procesando...');
            $estadoCarga.show();

            bootstrap.Modal.getInstance(document.getElementById('modalConfirmacion')).hide();

            const datosVenta = listaCarrito.map(item => ({
                idProducto: item.idProducto,
                cantidad: item.cantidad,
                precioVenta: item.precioVenta
            }));

            $.ajax({
                type: "POST",
                url: "VentaRapida.aspx/FinalizarVenta",
                data: JSON.stringify({ detallesVenta: datosVenta }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {
                    const mensaje = response.d;

                    if (mensaje.startsWith("Venta registrada")) {
                        Swal.fire('¡Venta Exitosa!', mensaje, 'success');
                        listaCarrito = [];
                    } else {
                        Swal.fire('Venta Fallida', mensaje, 'error');
                    }
                },

                error: function () {
                    Swal.fire('Error de Conexión', 'Hubo un error al procesar la venta.', 'error');
                },

                complete: function () {
                    $btn.prop('disabled', false).text('Confirmar Venta');
                    $estadoCarga.hide();
                    actualizarListaUI();
                }
            });
        }

        $(document).ready(function () {
            anadirItemALista(101, 'Casco Aéreo (Stock 1)', 300.00, 1);
            anadirItemALista(102, 'Guantes Pro (Stock 5)', 50.00, 2);
            actualizarListaUI();
        });

    </script>

</asp:Content>
