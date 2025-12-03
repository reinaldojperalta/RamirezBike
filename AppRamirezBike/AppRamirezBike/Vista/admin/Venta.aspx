<%@ Page Title="Historial de Ventas" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="AppRamirezBike.Vista.admin.Venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <!-- Título + Total + Select elegante para ordenar -->
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Historial de Ventas</h3>
        <div class="d-flex align-items-center gap-3">

            <span class="badge bg-success fs-5 px-4 py-2">
                Total Vendido: $<span id="totalVentas">0</span>
            </span>

            <div>
                <label class="form-label mb-1 text-muted small">Ordenar por fecha:</label>
                <select id="ordenarFecha" class="form-select form-select-sm" style="width: 220px;">
                    <option value="desc">Más recientes </option>
                    <option value="asc">Más antiguas </option>
                </select>
            </div>
        </div>
    </div>

    <div class="card shadow-sm">
        <div class="card-body p-0">
            <table id="tablaVentas" class="table table-striped table-hover table-sm mb-0" style="width:100%">
                <thead class="table-dark text-white">
                    <tr>
                        <th class="text-center">Pedido</th>
                        <th class="text-center">Fecha</th>
                        <th>Cliente</th>
                        <th>Producto</th>
                        <th class="text-center">Cant.</th>
                        <th class="text-end">Precio</th>
                        <th class="text-end">Subtotal</th>
                        <th class="text-end">Total</th>
                        <th class="text-center">Estado</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            var tabla = $('#tablaVentas').DataTable({
                responsive: true,
                ajax: {
                    url: 'Venta.aspx/ListarTodasLasVentas',
                    type: 'POST',
                    contentType: 'application/json',
                    data: function () { return JSON.stringify({}); },
                    dataSrc: 'd'
                },
                columns: [
                    { data: 'IdOrden', className: 'text-center fw-bold' },
                    {
                        data: 'FechaCreacion',
                        className: 'text-center text-nowrap',
                        render: function (data) {
                            var fecha = new Date(data);
                            if (isNaN(fecha)) {
                                var num = data.replace(/\/Date\((\d+).*\)\//, '$1');
                                fecha = new Date(parseInt(num));
                            }
                            return fecha.toLocaleString('es-CO');
                        }
                    },
                    { data: 'Cliente', render: d => '<strong>' + d + '</strong>' },
                    { data: 'Producto' },
                    { data: 'Cantidad', className: 'text-center' },
                    { data: 'PrecioUnitario', className: 'text-end', render: d => '$' + Number(d).toLocaleString('es-CO') },
                    { data: 'Subtotal', className: 'text-end', render: d => '$' + Number(d).toLocaleString('es-CO') },
                    { data: 'Total', className: 'text-end fw-bold', render: d => '<strong class="text-success">$' + Number(d).toLocaleString('es-CO') + '</strong>' },
                    {
                        data: 'Estado',
                        className: 'text-center',
                        render: d => d === 'Pagada'
                            ? '<span class="badge bg-success fs-6 px-3 py-2">PAGADA</span>'
                            : '<span class="badge bg-warning text-dark fs-6 px-3 py-2">PENDIENTE</span>'
                    }
                ],
                order: [[1, 'desc']],  
                pageLength: 25,
                language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                dom: 'Bfrtip',
                buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
                initComplete: function () {
                    var total = 0;
                    tabla.column(7).data().each(v => total += Number(v));
                    $('#totalVentas').text(total.toLocaleString('es-CO'));
                }
            });

            $('#ordenarFecha').on('change', function () {
                var valor = $(this).val(); // 'desc' o 'asc'
                tabla.order([1, valor]).draw();
            });
        });
    </script>
</asp:Content>