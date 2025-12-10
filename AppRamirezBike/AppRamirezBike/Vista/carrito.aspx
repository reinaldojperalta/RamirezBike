<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="AppRamirezBike.Vista.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">
    <!-- Contenedor principal con padding -->
    <div class="container mt-5 p-4">
        <h1 class="text-center mb-4 text-white fw-bold">Mi Carrito de Compras</h1>

        <!-- Panel Vacío: Se le aplica el estilo glass -->
        <asp:Panel ID="pnlVacio" runat="server" Visible="false">
            <div class="text-center p-5 glass-card rounded shadow">
                <h2 class="text-white">Tu carrito está vacío</h2>
                <a href="Catalogo.aspx" class="btn btn-red-glass btn-lg mt-3">Ir al Catálogo</a>
            </div>
        </asp:Panel>

        <!-- Panel con Productos: Se aplica estilo a la tabla -->
        <asp:Panel ID="pnlCarrito" runat="server" Visible="false">
            <!-- Se aplica una clase glass específica para la tabla -->
            <table class="table glass-table">
                <!-- Se reemplaza la clase de Bootstrap por una personalizada -->
                <thead class="glass-table-header">
                    <tr >
                        <th class="lead text-white">Imagen</th>
                        <th class="lead text-white">Producto</th>
                        <th class="lead text-white">Precio</th>
                        <th class="lead text-white">Cantidad</th>
                        <th class="lead text-white">Subtotal</th>
                        <th class="lead text-white">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCarrito" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src='img/<%# Eval("ImgUrl") %>' width="80" class="rounded" />
                                </td>
                                <td class="align-middle text-white"><strong><%# Eval("Nombre") %></strong></td>
                                <td class="align-middle text-white">$<%# Eval("Precio", "{0:N0}") %></td>
                                <td class="align-middle text-center text-white"><%# Eval("Cantidad") %></td>
                                <td class="align-middle text-danger fw-bold">$<%# Eval("Subtotal", "{0:N0}") %></td>
                                <td class="align-middle text-center">
                                    <!-- Se aplican las nuevas clases a los botones existentes -->
                                    <button type="button" class="btn-quantity-glass"
                                        onclick="modificarCantidad(<%# Eval("IdProducto") %>, -1)">
                                        –
                                    </button>
                                    <span class="mx-3 text-white fw-bold"><%# Eval("Cantidad") %></span>
                                    <button type="button" class="btn-quantity-glass"
                                        onclick="modificarCantidad(<%# Eval("IdProducto") %>, 1)">
                                        +
                                    </button>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <!-- Resumen de Pago: Se aplica estilo glass -->
            <div class="text-end mt-4 p-3 glass-card d-inline-block rounded">
                <h3 class="text-white">Total: <span class="text-danger">$<asp:Label ID="lblTotal" runat="server" /></span></h3>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold d-block mt-2" />
                <button type="button" class="btn btn-outline-light btn-lg me-3" onclick="vaciarCarrito()">
                    Vaciar Carrito
                </button>
                <asp:HiddenField ID="hdnCarrito" runat="server" />
                <asp:Button ID="btnPagar" runat="server" CssClass="btn btn-red-glass btn-lg"
                    Text="Proceder al Pago" OnClick="btnPagar_Click" />
            </div>
        </asp:Panel>
    </div>

    <script>
        // Tu JavaScript original se mantiene sin cambios
        function enviarCarritoAlServidor() {
            let carrito = localStorage.getItem("carrito_ramirez");
            document.getElementById("<%= hdnCarrito.ClientID %>").value = carrito;
        }
        document.addEventListener("DOMContentLoaded", enviarCarritoAlServidor);
    </script>

</asp:Content>