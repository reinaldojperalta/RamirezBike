<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="AppRamirezBike.Vista.admin.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Ventas</h3>
    </div>

    <div class="card">
        <div class="card-body">
            <table id="tablaVentas" class="table table-striped table-hover" style="width: 100%">
                <thead>
                    <tr>
                        <th></th>
                        <th>ID Orden</th>
                        <th>Referencia</th>
                        <th>ID Usuario</th>
                        <th>Total</th>
                        <th>Estado</th>
                        <th>Fecha Creación</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">

        // FIX 1: Corregida la URL al nombre de archivo correcto (Venta.aspx)
        const pageUrl = "Venta.aspx";
        let tablaVentas;

        // ----------------------------------------------------
        // FUNCIÓN DE FORMATO PARA EL DETALLE (FILA EXPANDIBLE)
        // ----------------------------------------------------
        function format(idOrden) {
            // Retorna un HTML temporal mientras se carga el detalle
            return `<div class="p-3">
                <h5>Detalle de Orden #${idOrden}</h5>
                <table id="detalle-${idOrden}" class="table table-bordered table-sm">
                    <thead>
                        <tr>
                            <th>ID Producto</th>
                            <th>Cantidad</th>
                            <th>Precio Unitario</th>
                            <th>Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td colspan="4">Cargando detalle...</td></tr>
                    </tbody>
                </table>
            </div>`;
        }

        $(document).ready(function () {

            // 1. INICIALIZAR DATATABLE
            function inicializarDataTableVentas() {
                tablaVentas = $('#tablaVentas').DataTable({
                    processing: true,
                    serverSide: false,
                    ajax: {
                        url: pageUrl + '/ListarVentas', // WebMethod 1
                        method: 'POST',
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        // FIX 2: Se agrega el payload vacío para asegurar la comunicación con el WebMethod
                        data: function () {
                            return JSON.stringify({});
                        },
                        dataSrc: function (json) { return json.d; },
                        error: function (xhr, error, thrown) {
                            // Intenta mostrar más detalles del error de red
                            const errorDetail = xhr.status === 404 ? " (Error 404: WebMethod no encontrado. ¿URL/Nombre correcto?)" : "";
                            console.error("Error AJAX de DataTables:", thrown, xhr);
                            alert("Error al cargar la tabla de Ventas." + errorDetail);
                        }
                    },
                    columns: [
                        {
                            // Columna de control
                            className: 'dt-control',
                            orderable: false,
                            data: null,
                            defaultContent: '<i class="bi bi-plus-circle"></i>' // Ícono para expandir
                        },
                        { data: 'IdOrden' },
                        { data: 'Referencia' },
                        { data: 'IdUsuario' },
                        {
                            data: 'Total',
                            render: $.fn.dataTable.render.number('.', ',', 2, '$') // Formato de moneda
                        },
                        { data: 'Estado' },
                        {
                            data: 'FechaCreacion',
                            render: function (data) {
                                // Se mantiene la lógica de DataTables/JSON para fechas .NET
                                return data ? new Date(parseInt(data.substr(6))).toLocaleDateString() : 'N/A';
                            }
                        }
                    ],
                    order: [[1, 'desc']], // Ordenar por ID Orden descendente
                    language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                    dom: 'Bfrtip',
                    buttons: ['copy', 'excel', 'pdf']
                });
            }

            // Inicialización al cargar el DOM
            inicializarDataTableVentas();

            // ----------------------------------------------------
            // 2. MANEJO DEL EVENTO CLIC PARA EXPANDIR LA FILA
            // ----------------------------------------------------
            $('#tablaVentas tbody').on('click', 'td.dt-control', function () {
                const tr = $(this).closest('tr');
                const row = tablaVentas.row(tr);
                const icon = $(this).find('i');

                if (row.child.isShown()) {
                    // Esta fila ya está abierta, la cerramos
                    row.child.hide();
                    tr.removeClass('shown');
                    icon.removeClass('bi-dash-circle').addClass('bi-plus-circle');
                } else {
                    // Abrimos la fila (muestra la estructura HTML inicial)
                    const idOrden = row.data().IdOrden;
                    row.child(format(idOrden)).show();
                    tr.addClass('shown');
                    icon.removeClass('bi-plus-circle').addClass('bi-dash-circle');

                    // Llamada AJAX para obtener el detalle real
                    cargarDetalleVenta(idOrden, $(`#detalle-${idOrden} tbody`));
                }
            });

            // ----------------------------------------------------
            // 3. FUNCIÓN PARA CARGAR EL DETALLE DE LA ORDEN
            // ----------------------------------------------------
            function cargarDetalleVenta(idOrden, detalleBody) {
                $.ajax({
                    url: pageUrl + '/ObtenerDetalleVenta', // WebMethod 2
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ idOrden: idOrden }),
                    success: function (response) {
                        detalleBody.empty(); // Limpiar el "Cargando..."

                        if (!response.d || response.d.length === 0) {
                            detalleBody.append('<tr><td colspan="4">No se encontraron detalles para esta orden.</td></tr>');
                            return;
                        }

                        // Llenar la tabla de detalle con los datos
                        $.each(response.d, function (i, detalle) {
                            detalleBody.append(`
                                <tr>
                                    <td>${detalle.IdProducto}</td>
                                    <td>${detalle.Cantidad}</td>
                                    <td>$${detalle.PrecioUnitario.toFixed(2)}</td>
                                    <td>$${detalle.Subtotal.toFixed(2)}</td>
                                </tr>
                            `);
                        });
                    },
                    error: function (err) {
                        detalleBody.empty().append(`<tr><td colspan="4" class="text-danger">Error al cargar el detalle: ${err.statusText}</td></tr>`);
                        console.error("Error al cargar detalle de venta:", err.responseText);
                    }
                });
            }

        });
    </script>
</asp:Content>