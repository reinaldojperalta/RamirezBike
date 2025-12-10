<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="AppRamirezBike.Vista.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">

    <!-- Contenedor principal con efecto glass -->
    <div class="container mt-5 p-4">
        <div class="row glass-card rounded p-4">

            <!-- Columna de la imagen con efecto neon -->
            <div class="col-md-6 mb-3 d-flex align-items-center justify-content-center">
                <div class="neon-glow-container rounded p-2">
                    <asp:Image runat="server" ID="imgPrincipal" CssClass="img-fluid rounded" />
                </div>
            </div>

            <!-- Columna de detalles -->
            <div class="col-md-6 d-flex flex-column justify-content-center">
                <h2 class="text-white fw-bold">
                    <asp:Label runat="server" ID="lblNombre" />
                </h2>

                <p class="text-white-50">
                    <asp:Label runat="server" ID="lblSKU" />
                </p>

                <div class="mb-3">
                    <span class="h3 me-2 text-danger fw-bold">
                        $<asp:Label runat="server" ID="lblPrecio" />
                    </span>
                    <asp:Label runat="server" ID="lblPrecioOriginal" CssClass="text-white-50 text-decoration-line-through" />
                </div>

                <p class="mb-4 text-white-50 flex-grow-1">
                    <asp:Label runat="server" ID="lblDescripcion" />
                </p>

                <div class="mb-3">
                    <label class="form-label text-white">Cantidad:</label>
                    <asp:TextBox runat="server" ID="txtCantidad"
                        ClientIDMode="Static"
                        CssClass="form-control glass-input"
                        TextMode="Number" Min="1"
                        Style="width: 120px;" />
                </div>

                <button type="button"
                    onclick="capturarDatosYAnadir(<%= producto.idProducto %>)"
                    class="btn btn-red-glass btn-lg w-100">
                    <i class="bi bi-cart-plus me-2"></i> AÑADIR AL CARRITO
                </button>
            </div>
        </div>
    </div>
</asp:Content>