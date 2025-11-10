<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="AppRamirezBike.Vista.RegistroUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registrarse</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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
                        <h3 class="text-center mb-4">Registrarse</h3>

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            CssClass="alert alert-warning"
                            HeaderText="<i class='bi bi-exclamation-circle me-2'></i> Por favor, Llene Todos Los Campos Del Formulario:" />

                        <div class="mb-3">
                            <asp:Label ID="lblTipoDocumento" runat="server" AssociatedControlID="ddlTipoDocumento" CssClass="form-label" Text="Tipo de Documento"></asp:Label>

                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccione un Tipo" Value=""></asp:ListItem>
                                <asp:ListItem Text="Cédula de Ciudadanía (CC)" Value="CC"></asp:ListItem>
                                <asp:ListItem Text="Tarjeta de Identidad (TI)" Value="TI"></asp:ListItem>
                                <asp:ListItem Text="Cédula de Extranjería (CE)" Value="CE"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="valTipoDocumento" runat="server"
                                ControlToValidate="ddlTipoDocumento"
                                InitialValue=""
                                ErrorMessage="El Tipo de Documento es obligatorio."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblDocumento" runat="server" AssociatedControlID="txtDocumento" CssClass="form-label" Text="Documento"></asp:Label>
                            <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valDocumento" runat="server"
                                ControlToValidate="txtDocumento"
                                ErrorMessage="El Documento es obligatorio."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="form-label" Text="Nombres"></asp:Label>
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valNombres" runat="server"
                                ControlToValidate="txtNombres"
                                ErrorMessage="Los Nombres son obligatorios."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="form-label" Text="Apellidos"></asp:Label>
                            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valApellidos" runat="server"
                                ControlToValidate="txtApellidos"
                                ErrorMessage="Los Apellidos son obligatorios."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valTelefono" runat="server"
                                ControlToValidate="txtTelefono"
                                ErrorMessage="El Teléfono es obligatorio."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblRol" runat="server" AssociatedControlID="ddlRol" CssClass="form-label" Text="Rol de Usuario"></asp:Label>
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="valRol" runat="server"
                                ControlToValidate="ddlRol"
                                InitialValue=""
                                ErrorMessage="Debe seleccionar un Rol de la lista."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Correo Electrónico"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valEmail" runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="El Correo Electrónico es obligatorio."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="mb-3">
                            <asp:Label ID="lblClave" runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valClave" runat="server"
                                ControlToValidate="txtClave"
                                ErrorMessage="La Contraseña es obligatoria."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="BtnRegistrarse" class="btnRegistrarse" runat="server" Text="Registrarse" OnClick="BtnRegistrarse_Click"/>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
