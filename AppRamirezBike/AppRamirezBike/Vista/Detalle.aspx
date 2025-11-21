<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="AppRamirezBike.Vista.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12 col-md-6">
                <div class="card shadow-sm">
                    <img src='img/<%= producto.imgUrl %>' class="card-img-top" alt='<%= producto.nombre %>' />
                    <div class="card-body">
                        <h4 class="card-title"><%= producto.nombre %></h4>
                        <p class="card-text"><%= producto.descripcion %></p>
                        <div class="d-flex justify-content-between">
                            <span class="fw-semibold">Precio:</span>
                            <span class="text-primary fw-bold"><%= producto.precio %></span>
                        </div>
                        <div class="d-flex justify-content-between mt-2">
                            <span class="fw-semibold">Stock disponible:</span>
                            <span class="badge bg-success"><%= producto.stock %></span>
                        </div>
                    </div>
                </div>

                <div class="mt-3 d-flex align-items-center justify-content-between">
                    <div class="d-flex align-items-center">
                        <button type="button" class="btn btn-outline-secondary" onclick="decrementar()">−</button>
                        <span id="cantidad" class="px-3 fs-5">1</span>
                        <button type="button" class="btn btn-outline-secondary" onclick="incrementar()">+</button>
                    </div>
                    <div class="text-end">
                        <div class="small text-muted">Subtotal</div>
                        <div class="fs-5 fw-bold" id="subtotal">0</div>
                    </div>
                </div>

                <div class="mt-3">
                    <button type="button" onclick="añadirAlCarrito(<%= producto.idProducto %>)" class="btn btn-primary w-100">AÑADIR AL CARRITO</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        const precioUnitario = <%= producto.precio %>;
        const maxStock = <%= producto.stock %>;
        let cantidadSel = 1;

        function actualizarSubtotal() {
            var cantidadEl = document.getElementById('cantidad');
            var subtotalEl = document.getElementById('subtotal');
            if (!cantidadEl || !subtotalEl) return;
            cantidadEl.innerText = cantidadSel;
            subtotalEl.innerText = (cantidadSel * precioUnitario);
        }

        function incrementar() {
            if (cantidadSel < maxStock) {
                cantidadSel++;
                actualizarSubtotal();
            } else {
                alert('No hay más stock disponible');
            }
        }

        function decrementar() {
            if (cantidadSel > 1) {
                cantidadSel--;
                actualizarSubtotal();
            }
        }

        document.addEventListener('DOMContentLoaded', actualizarSubtotal);
    </script>

</asp:Content>
