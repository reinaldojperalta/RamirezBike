<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="AppRamirezBike.Vista.RegistroUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registrarse</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>

    <style>
        body {
            background: white;
            min-height: 100vh;
            align-items: center;
            justify-content: center;
        }
        .card {
            border-radius: 50px;
            box-shadow: 0 1px 40px rgb(0, 0, 0);
        }
        .form-label {
            font-weight: 500;
        }
        .btnRegistrarse {
            background-color: white;
            box-shadow: 0 1px 10px rgb(0, 255, 33);
            border-radius: 10px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-5">
                    <div class="card p-4">
                        <h3 class="text-center mb-4">Registro de Usuario</h3>

                        <div class="mb-3">
                            <asp:Label ID="lblTipoDocumento" runat="server" AssociatedControlID="txtTipoDocumento" CssClass="form-label" Text="Tipo de Documento"></asp:Label>
                            <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblDocumento" runat="server" AssociatedControlID="txtDocumento" CssClass="form-label" Text="Documento"></asp:Label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="form-label" Text="Nombres"></asp:Label>
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="form-label" Text="Apellidos"></asp:Label>
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Correo Electrónico"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblClave" runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="BtnRegistrarse" class="btnRegistrarse" runat="server" Text="Registrarse" OnClick="BtnRegistrarse_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>