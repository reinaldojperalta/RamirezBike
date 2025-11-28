<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="AppRamirezBike.Vista.Checkout" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Confirmar Pedido</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- Script oficial de ePayco -->
    <script src="https://checkout.epayco.co/checkout.js"></script>

    <style>
        body {
            background-color: #f8f9fa;
        }
        .container {
            max-width: 600px;
            margin-top: 50px;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 4px 15px rgba(0,0,0,0.1);
        }
        h2 {
            color: #343a40;
            margin-bottom: 20px;
        }
        .total-label {
            font-size: 1.2rem;
        }
        .btn-success {
            width: 100%;
            padding: 10px;
            font-size: 1.1rem;
        }
        #lblMensaje {
            margin-top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Resumen del Pedido</h2>

            <p class="total-label">
                <strong>Total a pagar:</strong> <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"></asp:Label>
            </p>

            <!-- Valores que llenará el servidor -->
            <asp:HiddenField ID="hdnIdOrden" runat="server" />
            <asp:HiddenField ID="hdnTotal" runat="server" />
            <asp:HiddenField ID="hdnReferencia" runat="server" />

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>

            <!-- Botón server-side que inyectará script para abrir ePayco -->
            <asp:Button ID="btnIniciarPago" runat="server" CssClass="btn btn-success mt-3" Text="Pagar con ePayco" OnClick="btnIniciarPago_Click" />
        </div>
    </form>

    <!-- Bootstrap JS y dependencias (opcional si necesitas funcionalidad JS) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>