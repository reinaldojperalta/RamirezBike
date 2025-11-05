<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="AppRamirezBike.Vista.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">
            <div class="container mt-5">
            <div class="row">

                <asp:Repeater ID="rptProducto" runat="server">

                    <ItemTemplate>
                        <div class="col-sm-6 col-md-4 mb-4">
                            <div class="card shadow-sm h-100">

                                <img src='img/<%# Eval("imgUrl") %>' class="card-img-top" alt='<%# Eval("nombre") %>'>

                                <div class="card-body d-flex flex-column">

                                    <h4 class="card-title"><%# Eval("nombre") %></h4>

                                    <p class="card-text"><%# Eval("descripcion") %></p>

                                    <h5 class="card-text"><%# Eval("precio") %></h5>

                                    <div class="mt-auto">
                                        <a href="Catalogo.aspx?id=<%# Eval("idProducto") %>" class="btn btn-primary w-100">Comprar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
</asp:Content>
