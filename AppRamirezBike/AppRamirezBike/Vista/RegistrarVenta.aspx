<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarVenta.aspx.cs" Inherits="AppRamirezBike.Vistas.RegistrarVenta" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Venta - Ramirez Bike</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        function ConfirmarVenta() {
            var resultado = confirm("⚠️ ¿Está seguro de finalizar la venta y registrar el pago? Esta acción es irreversible y actualizará el stock de productos.");
            return resultado; 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="container mt-5"> 
            <h1 class="mb-4">🛍️ Registro de Nueva Venta</h1>
            <hr class="mb-4" />

            <div class="d-flex gap-3">

                <%-- ========== PANEL IZQUIERDO: SELECCIÓN DE PRODUCTOS Y CARRITO (Catálogo) ========== --%>
                <div class="w-75"> 

                    <h2 class="h4 mb-3">1. Búsqueda y Selección de Producto</h2>

                    <div class="mb-3 d-flex gap-2">
                        <asp:TextBox ID="txtBuscarProducto" runat="server" placeholder="Nombre o ID de producto" CssClass="form-control"></asp:TextBox>
                        
                        <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar" OnClick="BtnBuscarProducto_Click" CssClass="btn btn-secondary" />
                    </div>

                    <h3 class="h5 mt-4">Catálogo de Productos</h3>
                    <asp:GridView ID="gridProductos" runat="server"
                        AutoGenerateColumns="False"
                        Visible="False" OnRowCommand="gridProductos_RowCommand" EmptyDataText="No se encontraron productos disponibles o el catálogo está vacío."
                        CssClass="table table-striped table-hover table-bordered w-100">
                        <HeaderStyle BackColor="#343a40" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="idProducto" HeaderText="ID" ReadOnly="True" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ReadOnly="True" />
                            <asp:BoundField DataField="stock" HeaderText="Stock" ReadOnly="True" ItemStyle-Width="70px" />
                            <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" ReadOnly="True" ItemStyle-HorizontalAlign="Right" />

                            <asp:TemplateField HeaderText="Acción" ItemStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAgregar" runat="server"
                                        CommandName="Añadir"
                                        CommandArgument='<%# Eval("idProducto") %>'
                                        Text="🛒 Añadir" CssClass="btn btn-sm btn-info text-white" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                    <h2 class="h4 mt-5 mb-3">2. Detalles del Carrito</h2>

                    <asp:GridView ID="gridCarrito" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="El carrito de compras está vacío."
                        CssClass="table table-striped table-hover table-bordered w-100"
                        OnRowCommand="gridCarrito_RowCommand">
                        <HeaderStyle BackColor="#007bff" ForeColor="White" />
                        <Columns>
                            <asp:BoundField DataField="idProducto" HeaderText="ID" ReadOnly="True" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="nombreProducto" HeaderText="Producto" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Cantidad" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("cantidad") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="precioUnitario" HeaderText="P. Unitario" DataFormatString="{0:C}" ReadOnly="True" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="subTotal" HeaderText="Subtotal" DataFormatString="{0:C}" ReadOnly="True" ItemStyle-HorizontalAlign="Right" />
                            
                            <asp:TemplateField HeaderText="Acción" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnQuitar" runat="server"
                                        CommandName="Quitar" 
                                        CommandArgument='<%# Eval("idProducto") %>'
                                        Text="❌ Quitar" CssClass="btn btn-sm btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </div>

                <%-- ========== PANEL DERECHO: RESUMEN Y CONFIRMACIÓN ========== --%>
                <div class="w-25">
                    <div class="card p-4 shadow">
                        <h2 class="h4 card-title mb-4">3. Resumen y Pago</h2>

                        <div class="mb-3">
                            <label for="ddlCliente" class="form-label">Seleccionar Cliente:</label>
                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select">
                                <asp:ListItem Value="1" Text="Cliente Público (ID: 1)"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Juan Pérez (ID: 10)"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <hr />

                        <div class="d-flex justify-content-between mb-2">
                            <label class="fw-normal">Subtotal Carrito:</label>
                            <asp:Label ID="lblSubtotal" runat="server" Text="$0.00" CssClass="fw-bold"></asp:Label>
                        </div>

                        <div class="d-flex justify-content-between mb-2">
                            <label class="fw-normal">Impuestos (19%):</label>
                            <asp:Label ID="lblImpuesto" runat="server" Text="$0.00" CssClass="fw-bold"></asp:Label>
                        </div>
                        
                        <hr class="my-3" />

                        <div class="d-flex justify-content-between align-items-center">
                            <h3 class="h4 text-muted">TOTAL A PAGAR:</h3>
                            <asp:Label ID="lblTotalVenta" runat="server" Text="$0.00" CssClass="display-6 text-success fw-bold"></asp:Label>
                        </div>

                        <hr />

                        <%-- BOTÓN DE CONFIRMACIÓN DE PAGO  --%>
                        <asp:Button
                            ID="BtnConfirmarPago"
                            runat="server"
                            Text="✅ Confirmar Pago y Registrar Venta"
                            OnClick="BtnConfirmarPago_Click"
                            CssClass="btn btn-success btn-lg w-100 mt-3" 
                            OnClientClick="return ConfirmarVenta();"
                        />
                        
                        <asp:Label ID="lblMensajeSistema" runat="server" ForeColor="Red" CssClass="mt-2"></asp:Label>

                    </div>
                </div>

            </div>
        </div>
    </form>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>