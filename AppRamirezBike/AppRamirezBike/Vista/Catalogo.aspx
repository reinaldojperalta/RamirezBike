<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="AppRamirezBike.Vista.Catalogo" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">

    <!-- ===================================================== -->
    <!-- JUMBOTRON DE BIENVENIDA -->
    <!-- ===================================================== -->
    <div class="container mt-4">
      <div class="p-5 rounded shadow-sm glass-card">
        <h1 class="display-4 text-white">Bienvenido a RamirezBike</h1>
        <p class="lead text-white">
          Tu tienda de confianza en bicicletas, refacciones y accesorios.
          Calidad, precio justo y atención personalizada para cada pedal.
        </p>
        <hr class="my-4 border-white">
        <p class="lead text-white">Envíos a todo el país · Financiación sin intereses · Servicio técnico propio</p>
      </div>
    </div>

    <!-- ===================================================== -->
    <!-- CONTENEDOR PRINCIPAL CON SIDEBAR Y GRID DE PRODUCTOS -->
    <!-- ===================================================== -->
    <div class="container mt-5">
        <div class="row">
            
            <!-- ===================================================== -->
            <!-- SIDEBAR DE FILTROS -->
            <!-- ===================================================== -->
            <aside class="col-lg-3 mb-4">
                <!-- Aplicamos glass-card al sidebar -->
                <div class="card h-100 glass-card">
                    <div class="card-header border-0 bg-transparent">
                        <h4 class="mb-0 text-white">Filtrar Productos</h4>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:Label ID="lblFiltro" runat="server" Text="Categoría" CssClass="form-label text-white" AssociatedControlID="ddlCategorias" />
                            <!-- Aplicamos una clase personalizada al dropdown -->
                            <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="form-select glass-select"></asp:DropDownList>
                        </div>
                        <!-- Espacio para futuros filtros -->
                        <div class="mb-3">
                        </div>
                    </div>
                </div>
            </aside>

            <!-- ===================================================== -->
            <!-- GRID DE PRODUCTOS -->
            <!-- ===================================================== -->
            <main class="col-lg-9">
                <div class="row g-4">
                    <asp:Repeater ID="rptProducto" runat="server">
                    <ItemTemplate>
                        <div class="col-xl-4 col-lg-6 col-md-6">
                            <div class="card h-100 shadow-sm transition-hover glass-card">
                                <img src='img/<%# Eval("imgUrl") %>' class="card-img-top img-fluid" alt='<%# Eval("nombre") %>' style="height: 200px; object-fit: cover;">
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title text-white"><%# Eval("nombre") %></h5>
                                    <p class="card-text flex-grow-1 text-white-50"><%# TruncateDescription(Eval("descripcion").ToString(), 100) %></p>
                                    <h4 class="card-text text-danger fw-bold">$<%# Eval("precio") %></h4>
                                    <div class="mt-auto d-grid gap-2">
                                        <a href="Detalle.aspx?id=<%# Eval("idProducto") %>" class="btn btn-red-glass">Ver Detalles</a>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!-- ===================================================== -->
                <!-- PAGINACIÓN -->
                <!-- ===================================================== -->
                <nav aria-label="Navegación de Catálogo" class="mt-4">
                    <ul class="pagination justify-content-center">
                        <asp:Repeater ID="rptPaginacion" runat="server">
                            <ItemTemplate>
                                <li class="page-item <%# EsPaginaActiva(Container.DataItem.ToString()) %>">
                                   <a class="page-link glass-page-link" href="Catalogo.aspx?pagina=<%# Container.DataItem %><%# BaseUrlFiltros %>"><%# Container.DataItem %></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </nav>
            </main>
        </div>
    </div>
</asp:Content>