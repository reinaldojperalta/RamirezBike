<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="AppRamirezBike.Vista.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">
    <div class="container mt-5">
        <h1 class="text-center mb-4 text-primary fw-bold">Mi Carrito de Compras</h1>

        <asp:Panel ID="pnlVacio" runat="server" Visible="false">
            <div class="text-center p-5 bg-light rounded shadow">
                <h2 class="text-muted">Tu carrito está vacío</h2>
                <a href="Catalogo.aspx" class="btn btn-primary btn-lg mt-3">Ir al Catálogo</a>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlCarrito" runat="server" Visible="false">
            <table class="table table-striped table-hover">
                <thead class="table-primary">
                    <tr>
                        <th>Imagen</th>
                        <th>Producto</th>
                        <th>Precio</th>
                        <th>Cantidad</th>
                        <th>Subtotal</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCarrito" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src='img/<%# Eval("ImgUrl") %>' width="80" class="rounded" /></td>
                                <td class="align-middle"><strong><%# Eval("Nombre") %></strong></td>
                                <td class="align-middle">$<%# Eval("Precio", "{0:N0}") %></td>
                                <td class="align-middle text-center"><%# Eval("Cantidad") %></td>
                                <td class="align-middle text-success fw-bold">$<%# Eval("Subtotal", "{0:N0}") %></td>
                                <td class="align-middle text-center">
                                    <button type="button" class="btn btn-sm btn-outline-danger"
                                        onclick="modificarCantidad(<%# Eval("IdProducto") %>, -1)">
                                        –</button>

                                    <span class="mx-3 fw-bold"><%# Eval("Cantidad") %></span>

                                    <button type="button" class="btn btn-sm btn-outline-primary"
                                        onclick="modificarCantidad(<%# Eval("IdProducto") %>, 1)">
                                        +</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <div class="text-end mt-4">
                <h3>Total: $<asp:Label ID="lblTotal" runat="server" CssClass="text-success fw-bold" /></h3>
                <button type="button" class="btn btn-danger btn-lg me-3"
                    onclick="vaciarCarrito()">
                    Vaciar Carrito
                </button>
                <button class="btn btn-success btn-lg">Proceder al Pago</button>
            </div>
        </asp:Panel>
    </div>

   
</asp:Content>

