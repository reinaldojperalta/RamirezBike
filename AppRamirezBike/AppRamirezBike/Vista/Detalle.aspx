<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="AppRamirezBike.Vista.Detalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">
    <button type="button" 
        onclick="añadirAlCarrito(<%= producto.idProducto %>, '<%= producto.nombre %>', <%= producto.precio %>, '<%= producto.imgUrl %>')"
        class="btn-agregar-carrito">
        AÑADIR AL CARRITO
    </button>
    <script src="js/carrito.js"></script>
</asp:Content>
