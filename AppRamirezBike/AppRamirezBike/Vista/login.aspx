<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/loginMaster.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AppRamirezBike.Vista.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-wrapper {
            max-width: 400px;
            background-color: white !important;
            border: 2px solid #ddd !important;
            opacity: 0;
        }
        .login-logo {
            max-height: 80px;
            width: auto;
            object-fit: contain;
        }
        .btn-custom-red {
            background-color: #000000 !important;
            border-color: #000000 !important;
            color: white !important;
            transition: background-color 0.3s ease;
        }
        .btn-custom-red:hover {
            background-color: #ff3333 !important;
            border-color: #ff3333 !important;
            color: white !important;
        }
        .form-control-light {
            background-color: white;
            color: #333333;
            border-color: #ced4da;
        }
        .form-control-light::placeholder {
            color: #6c757d;
        }
        .form-control-light:focus {
             background-color: white;
             color: #333333;
             border-color: #cc0000;
             box-shadow: 0 0 0 0.25rem rgba(204, 0, 0, 0.25);
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container d-flex justify-content-center align-items-center" style="min-height: 90vh;">
        
        <div class="card login-wrapper shadow-lg rounded-4 w-100">
            <div class="card-body p-4 p-md-5">

                <div class="text-center mb-4 mb-md-5">
                    <img src="img/logo.png" alt="Logo Ramirez Bike" class="login-logo mb-3" />
                    
                    <h2 class="card-title fw-bolder text-center" style="font-size: 1.8rem; color: #cc0000;">
                        INICIAR SESIÓN
                    </h2>
                    <p class="text-secondary small">
                        Ingresa tus credenciales para acceder al sistema.
                    </p>
                </div>
                
                <div class="mb-3">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label fw-bold text-dark" Text="Correo Electrónico"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg form-control-light" TextMode="Email" placeholder="ejemplo@correo.com"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valEmail" runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="El Correo Electrónico es obligatorio."
                        Text="Campo Requerido" CssClass="text-danger small mt-1" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-4">
                    <asp:Label ID="lblClave" runat="server" AssociatedControlID="txtClave" CssClass="form-label fw-bold text-dark" Text="Contraseña"></asp:Label>
                    <asp:TextBox ID="txtClave" runat="server" CssClass="form-control form-control-lg form-control-light" TextMode="Password" placeholder="••••••••"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valClave" runat="server"
                        ControlToValidate="txtClave"
                        ErrorMessage="La Contraseña es obligatoria."
                        Text="Campo Requerido" CssClass="text-danger small mt-1" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="d-grid mt-4">
                    <asp:Button ID="BtnLogin"
                        runat="server"
                        Text="INGRESAR AL SISTEMA"
                        CssClass="btn btn-lg fw-bold rounded-pill btn-custom-red"
                        OnClick="BtnLogin_Click" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-bold small"></asp:Label>
                </div>
                
            </div>
        </div>
    </div>
        <script type="text/javascript">
            window.onload = function () {
                setTimeout(function () {
                    var loginCard = document.querySelector('.card.login-wrapper');
                    if (loginCard) {
                        loginCard.classList.add('animate-login');
                    }
                }, 50);
            };
        </script>
</asp:Content>
