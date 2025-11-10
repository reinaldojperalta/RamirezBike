<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/loginMaster.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AppRamirezBike.Vista.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    <div class="d-grid mt-4">
        <asp:Button ID="BtnLogin"
            runat="server"
            Text="Ingreso"
            CssClass="btn btn-primary"
            OnClick="BtnLogin_Click" />
    </div>

    <div class="mt-3">
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
    </div>

</asp:Content>
