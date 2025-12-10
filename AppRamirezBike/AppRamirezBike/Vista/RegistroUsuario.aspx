<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="AppRamirezBike.Vista.RegistroUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Registrarse - RamirezBike</title>
    
    <!-- SweetAlert2 -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <!-- Nuestro CSS con todos los efectos -->
    <link href="~/assets/css/effects.css" rel="stylesheet" type="text/css" />

    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
    <!-- Bootstrap Icons -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    <style>
        /* --- ESTILOS PARA EL CENTRADO Y FONDO --- */
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        body {
            background-color: transparent !important;
            display: flex;
            align-items: center;
            justify-content: center;
            overflow: auto; /* Permite hacer scroll si el contenido es muy alto */
        }

        /* Contenedor principal que centra el formulario */
        .form-container {
            width: 100%;
            max-width: 450px; /* Ancho máximo para el formulario */
            padding: 20px;
        }

        /* --- ESTILOS ESPECÍFICOS DE ESTA PÁGINA --- */
        .glass-card h3 {
            color: #ffffff;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
        }

        .form-label {
            color: #ffffff;
            font-weight: 500;
        }

        /* Aseguramos que los mensajes de error de los validadores sean visibles */
        .text-danger {
            color: #ff6b6b !important;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.8);
            font-size: 0.9em;
        }
    </style>
</head>
<body>

    <!-- ===================================================== -->
    <!-- FONDO ANIMADO (COPIADO DE LA MASTER PAGE) -->
    <!-- ===================================================== -->
    <div class="ethereal-background">
        <div class="filter-container">
            <svg style="position: absolute;">
                <defs>
                    <filter id="ethereal-filter">
                        <feTurbulence
                            result="undulation"
                            numOctaves="2"
                            baseFrequency="0.0005, 0.002"
                            seed="1"
                            type="turbulence"
                        />
                        <feColorMatrix
                            id="color-matrix"
                            in="undulation"
                            type="hueRotate"
                            values="0"
                        />
                        <feColorMatrix
                            in="dist"
                            result="circulation"
                            type="matrix"
                            values="4 0 0 0 1  4 0 0 0 1  4 0 0 0 1  1 0 0 0 0"
                        />
                        <feDisplacementMap
                            in="SourceGraphic"
                            in2="circulation"
                            scale="30"
                            result="dist"
                        />
                        <feDisplacementMap
                            in="dist"
                            in2="undulation"
                            scale="30"
                            result="output"
                        />
                    </filter>
                </defs>
            </svg>
            <div class="masked-layer"></div>
        </div>
        <div class="noise-overlay"></div>
    </div>

    <form id="form1" runat="server">
        <div class="form-container">
            <!-- Logo de la empresa -->
            <div class="text-center mb-4">
                <img src="/Vista/img/logo.png" alt="RamirezBike Logo" width="80" height="80" class="rounded-circle">
            </div>

            <!-- Tarjeta principal con efecto glass -->
            <div class="card p-4 glass-card">
                <h3 class="text-center mb-4">Registrarse</h3>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    CssClass="alert alert-danger"
                    HeaderText="<i class='bi bi-exclamation-triangle-fill me-2'></i> Por favor, corrija los siguientes errores:" />

                <div class="mb-3">
                    <asp:Label ID="lblTipoDocumento" runat="server" AssociatedControlID="ddlTipoDocumento" CssClass="form-label" Text="Tipo de Documento"></asp:Label>
                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-select glass-select">
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
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control glass-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valDocumento" runat="server"
                        ControlToValidate="txtDocumento"
                        ErrorMessage="El Documento es obligatorio."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="form-label" Text="Nombres"></asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control glass-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valNombres" runat="server"
                        ControlToValidate="txtNombres"
                        ErrorMessage="Los Nombres son obligatorios."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="form-label" Text="Apellidos"></asp:Label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control glass-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valApellidos" runat="server"
                        ControlToValidate="txtApellidos"
                        ErrorMessage="Los Apellidos son obligatorios."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control glass-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valTelefono" runat="server"
                        ControlToValidate="txtTelefono"
                        ErrorMessage="El Teléfono es obligatorio."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <%-- El div comentado se mantiene sin modificar como solicitaste --%>
<%--                        <div class="mb-3">
                            <asp:Label ID="lblRol" runat="server" AssociatedControlID="ddlRol" CssClass="form-label" Text="Rol de Usuario"></asp:Label>
                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="valRol" runat="server"
                                ControlToValidate="ddlRol"
                                InitialValue=""
                                ErrorMessage="Debe seleccionar un Rol de la lista."
                                Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>--%>

                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Correo Electrónico"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control glass-input" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="El Correo Electrónico es obligatorio."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblClave" runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control glass-input" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valClave" runat="server"
                        ControlToValidate="txtClave"
                        ErrorMessage="La Contraseña es obligatoria."
                        Text="Obligatorio" CssClass="text-danger" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="d-grid">
                    <asp:Button ID="BtnRegistrarse" class="btn btn-red-glass btn-lg" runat="server" Text="Registrarse" OnClick="BtnRegistrarse_Click"/>
                </div>
            </div>
        </div>
    </form>

    <!-- Script para la animación del fondo (COPIADO DE LA MASTER PAGE) -->
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const colorMatrix = document.getElementById('color-matrix');
            if (colorMatrix) {
                let hue = 0;
                function animate() {
                    hue = (hue + 1.6) % 360;
                    colorMatrix.setAttribute('values', hue);
                    requestAnimationFrame(animate);
                }
                animate();
            }
        });
    </script>

    <!-- Bootstrap 5 JS Bundle -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
</body>
</html>