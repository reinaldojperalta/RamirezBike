<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmacionEpayco.aspx.cs" Inherits="AppRamirezBike.Vista.ConfirmacionEpayco" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <title>Confirmación de Pago</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body { background-color: #f8f9fa; }
        .container { max-width: 600px; margin-top: 50px; }
        .estado-Pagada { color: green; font-weight: bold; }
        .estado-Rechazada { color: red; font-weight: bold; }
        .estado-Pendiente { color: orange; font-weight: bold; }
        .estado-Fallida { color: darkred; font-weight: bold; }
        .estado-Desconocido { color: gray; font-weight: bold; }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container card p-4 shadow-sm bg-white">
            <h2 class="mb-4">Estado del Pago</h2>

            <div class="mb-2">
                <strong>Estado:</strong> <asp:Label ID="lblEstado" runat="server" CssClass=""></asp:Label>
            </div>
            <div class="mb-2">
                <strong>Referencia:</strong> <asp:Label ID="lblReferencia" runat="server"></asp:Label>
            </div>
            <div class="mb-2">
                <strong>Valor:</strong> <asp:Label ID="lblValor" runat="server"></asp:Label>
            </div>
            <div class="mb-4">
                <strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server"></asp:Label>
            </div>

            <asp:Button ID="btnVolver" runat="server" Text="Volver al Catálogo" CssClass="btn btn-primary" OnClick="btnVolver_Click" />
        </div>
    </form>
</body>
</html>
