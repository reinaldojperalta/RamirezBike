<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="AppRamirezBike.Vista.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">









<div class="container mt-5">
    <div class="row">
        <!-- Imagen -->
        <div class="col-md-6 mb-3">
            <asp:Image runat="server" ID="imgPrincipal"
                       CssClass="img-fluid rounded" />
        </div>

        <!-- Detalles -->
        <div class="col-md-6">
            <h2><asp:Label runat="server" ID="lblNombre" /></h2>
            <p class="text-muted">
                <asp:Label runat="server" ID="lblSKU" />
            </p>

            <div class="mb-3">
                <span class="h4 me-2">
                    <asp:Label runat="server" ID="lblPrecio" />
                </span>
                <asp:Label runat="server" ID="lblPrecioOriginal" />
            </div>

            <p class="mb-4">
                <asp:Label runat="server" ID="lblDescripcion" />
            </p>

            <div class="mb-3">
                <label class="form-label">Cantidad:</label>
                <asp:TextBox runat="server" ID="txtCantidad"
                             CssClass="form-control"
                             TextMode="Number" Min="1"
                             Style="width:90px;" />
            </div>

            <!-- botón HTML que ya conoces -->
            <button type="button" 
                onclick="añadirAlCarrito(<%= producto.idProducto %>)"
                class="btn btn-primary btn-lg">
                AÑADIR AL CARRITO
            </button>
        </div>
    </div>
</div>
 
</asp:Content>
