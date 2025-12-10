<%@ Page Title="Historial de Ventas" Language="C#" MasterPageFile="~/Vista/admin/dashboard.master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="AppRamirezBike.Vista.admin.Venta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSubPage" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Historial de Ventas</h3>
        <div class="d-flex align-items-center gap-3">
            <span class="badge bg-success fs-5 px-4 py-2">
                Total Vendido: $<span id="totalVentas">0</span>
            </span>

            <div>
                <label class="form-label mb-1 small text-muted">Ordenar por fecha:</label>
                <select id="filtroOrden" class="form-select form-select-sm" style="width: 220px;">
                    <option value="desc" selected>Más recientes</option>
                    <option value="asc">Más antiguas</option>
                </select>
            </div>
        </div>
    </div>

    <div class="card shadow-sm">
        <div class="card-body p-0">
            <table id="tablaVentas" class="table table-striped table-hover table-sm mb-0" style="width:100%">
                <thead class="table-dark text-white">
                    <tr>
                        <th></th>
                        <th class="text-center">Pedido</th>
                        <th class="text-center">Fecha</th>
                        <th>Cliente</th>
                        <th class="text-end">Total</th>
                        <th class="text-center">Estado</th>
                        <th>Método</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        const pageUrl = "Venta.aspx";
        let tabla;

        function format(d) {
            let html = `<div class="p-4 bg-light border-start border-primary border-4">
                <h6 class="fw-bold text-primary mb-3">Productos del Pedido #${d.IdOrden} (${d.Detalles.length} item${d.Detalles.length > 1 ? 's' : ''})</h6>
                <table class="table table-sm table-bordered">
                    <thead class="table-secondary">
                        <tr>
                            <th>Producto</th>
                            <th class="text-center">Cant.</th>
                            <th class="text-end">Precio</th>
                            <th class="text-end">Subtotal</th>
                        </tr>
                    </thead>
                    <tbody>`;
            for (let i = 0; i < d.Detalles.length; i++) {
                let item = d.Detalles[i];
                html += `<tr>
                    <td>${item.Producto}</td>
                    <td class="text-center">${item.Cantidad}</td>
                    <td class="text-end">$${Number(item.PrecioUnitario).toLocaleString('es-CO')}</td>
                    <td class="text-end">$${Number(item.Subtotal).toLocaleString('es-CO')}</td>
                </tr>`;
            }
            html += `</tbody></table></div>`;
            return html;
        }

        $(document).ready(function () {
            tabla = $('#tablaVentas').DataTable({
                ajax: {
                    url: pageUrl + '/ListarTodasLasVentas',
                    type: 'POST',
                    contentType: 'application/json',
                    data: function () { return JSON.stringify({}); },
                    dataSrc: 'd'
                },
                columns: [
                    { className: 'dt-control text-center', orderable: false, data: null, defaultContent: '<i class="bi bi-plus-circle text-primary fs-5"></i>' },
                    { data: 'IdOrden', className: 'text-center fw-bold' },
                    {
                        data: 'FechaCreacion',
                        render: function (data) {
                            try {
                                let date = new Date(data);
                                if (isNaN(date.getTime())) {
                                    let ticks = parseInt(data.replace(/\/Date\((\d+)(?:-\d+)?\)\//i, '$1'));
                                    date = new Date(ticks);
                                }
                                return date.toLocaleString('es-CO');
                            } catch (e) {
                                return 'Sin fecha';
                            }
                        },
                        className: 'text-center text-nowrap'
                    },
                    { data: 'Cliente', render: d => `<strong>${d}</strong>` },
                    { data: 'Total', render: d => `<strong class="text-success fs-5">$${Number(d).toLocaleString('es-CO')}</strong>`, className: 'text-end fw-bold' },
                    {
                        data: 'Estado',
                        render: d => d === 'Pagada'
                            ? '<span class="badge bg-success fs-6 px-3 py-2">PAGADA</span>'
                            : '<span class="badge bg-warning text-dark fs-6 px-3 py-2">PENDIENTE</span>',
                        className: 'text-center'
                    },
                    { data: 'MetodoPago', defaultContent: 'N/A' }
                ],
                order: [[2, 'desc']], // Más recientes primero
                language: { url: 'https://cdn.datatables.net/plug-ins/2.0.0/i18n/es-ES.json' },
                dom: 'Bfrtip',
                buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
                initComplete: function () {
                    let total = 0;
                    tabla.column(4).data().each(v => total += Number(v));
                    $('#totalVentas').text(total.toLocaleString('es-CO'));
                }
            });

            order: [[2, 'desc'], [1, 'desc']],

                // FILTRO
                $('#filtroOrden').on('change', function () {
                    let orden = $(this).val();
                    if (orden === 'desc') {
                        tabla.order([[2, 'desc'], [1, 'desc']]).draw(); // recientes
                    } else {
                        tabla.order([[2, 'asc'], [1, 'asc']]).draw(); // antiguas
                    }
                });

            // Al cargar
            tabla.order([[2, 'desc'], [1, 'desc']]).draw();

            $('#tablaVentas tbody').on('click', 'td.dt-control', function () {
                let tr = $(this).closest('tr');
                let row = tabla.row(tr);
                let icon = $(this).find('i');
                if (row.child.isShown()) {
                    row.child.hide();
                    icon.removeClass('bi-dash-circle').addClass('bi-plus-circle');
                } else {
                    row.child(format(row.data())).show();
                    icon.removeClass('bi-plus-circle').addClass('bi-dash-circle');
                }
            });
        });
    </script>
</asp:Content>