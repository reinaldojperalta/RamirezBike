<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="AppRamirezBike.Vistas.GestionProductos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestión de Productos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    
    <%-- 1. AÑADIDO: Librería SweetAlert2 para modales de notificación --%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h1>⚙️ Gestión de Inventario (Productos)</h1>
            <hr />

            <asp:HiddenField ID="hfIdProducto" runat="server" Value="" />

            <%-- ========================================================== --%>
            <%-- A. FORMULARIO DE CREACIÓN/EDICIÓN --%>
            <%-- ========================================================== --%>
            <div class="card p-4 mb-4">
                <asp:Label ID="lblTituloFormulario" runat="server" CssClass="h2">
    ✍️ Registrar Nuevo Producto
                </asp:Label>
                <%-- ELIMINADO: lblMensajeSistema ya que usaremos SweetAlert2 --%>

                <div class="row">
    <div class="col-md-6 mb-3">
        <label for="<%= txtNombre.ClientID %>">Nombre:</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>
    <div class="col-md-6 mb-3">
        <label for="<%= ddlCategoria.ClientID %>">Categoría:</label>
        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
            <asp:ListItem Value="1" Text="Bicicletas"></asp:ListItem>
            <asp:ListItem Value="2" Text="Accesorios"></asp:ListItem>
            <asp:ListItem Value="3" Text="Componentes"></asp:ListItem>
            <%-- Asegúrate de que tu lógica carga dinámicamente aquí --%>
        </asp:DropDownList>
    </div>
</div>

<div class="row">
    <div class="col-12 mb-3">
        <label for="<%= txtDescripcion.ClientID %>">Descripción:</label>
        <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />
    </div>
</div>

<div class="row">
    <div class="col-md-4 mb-3">
        <label for="<%= txtPrecio.ClientID %>">Precio Venta:</label>
        <asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" CssClass="form-control" />
    </div>
    <div class="col-md-4 mb-3">
        <label for="<%= txtPrecioProovedor.ClientID %>">Precio Proveedor (Costo):</label>
        <asp:TextBox ID="txtPrecioProovedor" runat="server" TextMode="Number" CssClass="form-control" />
    </div>
    <div class="col-md-4 mb-3">
        <label for="<%= txtStock.ClientID %>">Stock:</label>
        <asp:TextBox ID="txtStock" runat="server" TextMode="Number" CssClass="form-control" />
    </div>
</div>

<div class="row">
    <div class="col-12 mb-3">
        <label for="<%= FileInput.ClientID %>">Imagen del Producto:</label>
        <asp:FileUpload ID="FileInput" runat="server" CssClass="form-control" />
        <asp:Label ID="lblImagenActual" runat="server" CssClass="form-text text-muted"></asp:Label>
    </div>
</div>

                <asp:Button ID="btnGuardarProducto" runat="server"
                    Text="➕ Registrar Producto" OnClick="BtnGuardarProducto_Click"
                    CssClass="btn btn-primary btn-lg mt-3" />

                <asp:Button ID="btnCancelarEdicion" runat="server"
                    Text="❌ Cancelar Edición" OnClick="BtnCancelarEdicion_Click"
                    CssClass="btn btn-danger btn-sm mt-2" Visible="False" />
            </div>

            <%-- ========================================================== --%>
            <%-- B. LISTA DE PRODUCTOS --%>
            <%-- ========================================================== --%>
            <h2 class="mt-5">Inventario Actual</h2>
            <asp:GridView ID="gridProductos" runat="server"
                AutoGenerateColumns="False"
                OnRowCommand="gridProductos_RowCommand"
                CssClass="table table-striped table-hover table-bordered w-100">
                <HeaderStyle BackColor="#343a40" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="idProducto" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="stock" HeaderText="Stock" ItemStyle-Width="70px" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />

                    <asp:TemplateField HeaderText="Acción" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server"
                                CommandName="EditarProducto"
                                CommandArgument='<%# Eval("idProducto") %>'
                                Text="✏️ Editar" CssClass="btn btn-sm btn-warning" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>