<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="AppRamirezBike.Vista.Checkout" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Confirmar Pedido</title>

    <!-- Script oficial de ePayco -->
    <script src="https://checkout.epayco.co/checkout.js"></script>
</head>
<body>

<form id="form1" runat="server">

    <h2>Resumen del Pedido</h2>

    <p><strong>Total a pagar:</strong> <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"></asp:Label></p>

    <!-- Se usa en ePayco como referencia -->
    <asp:HiddenField ID="hdnIdOrden" runat="server" />
    <asp:HiddenField ID="hdnCarrito" runat="server" />
    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>


    <button type="button" class="btn btn-success" onclick="procesarPago()">Pagar con ePayco</button>

</form>

<script>

    function procesarPago() {

        var handler = ePayco.checkout.configure({
            key: "YOUR_PUBLIC_KEY_SANDBOX",
            test: true
        });

        handler.open({
            name: "Ramirez Bike Store",
            description: "Compra Online",
            invoice: document.getElementById("<%= hdnIdOrden.ClientID %>").value,
        currency: "cop",
        amount: document.getElementById("<%= lblTotal.ClientID %>").innerText,
        tax_base: "0",
        tax: "0",
        country: "CO",
        external: "false",
        test: "true",
        response: "https://localhost:44342/Vista/RespuestaEpayco.aspx",
        confirmation: "https://localhost:44342/Vista/ConfirmacionEpayco.aspx"
    });

    }
</script>

</body>
</html>