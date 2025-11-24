<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="AppRamirezBike.Vista.Catalogo" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">

    <!-- Jumbotron RamirezBike -->
<div class="container mt-3">
  <div class="mt-4 p-5 bg-dark text-white rounded shadow">
    <h1 class="display-4">Bienvenido a RamirezBike</h1>
    <p class="lead">
      Tu tienda de confianza en bicicletas, refacciones y accesorios.
      Calidad, precio justo y atención personalizada para cada pedal.
    </p>
    <hr class="my-4">
    <p>Envíos a todo el país · Financiación sin intereses · Servicio técnico propio</p>
    <!--<a class="btn btn-outline-light btn-lg" href="Catalogo.aspx" role="button">
      Ver catálogo
    </a>-->
  </div>
    <br />
      <div class ="filtro-categoria">
      <asp:Label ID="lblFiltro" runat="server" Text="Filtra Aqui Por Categoria"  />
      <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"></asp:DropDownList>
  </div>
</div>

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
                                    <a href="Detalle.aspx?id=<%# Eval("idProducto") %>" class="btn btn-primary w-100">Comprar</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
            <nav aria-label="Navegación de Catálogo">
                <ul class="pagination justify-content-center">
                    <asp:Repeater ID="rptPaginacion" runat="server">
                        <ItemTemplate>
                            <li class="page-item <%# EsPaginaActiva(Container.DataItem.ToString()) %>">
                               <a class="page-link" href="Catalogo.aspx?pagina=<%# Container.DataItem %><%# BaseUrlFiltros %>"><%# Container.DataItem %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div>
    </div>
</asp:Content>
