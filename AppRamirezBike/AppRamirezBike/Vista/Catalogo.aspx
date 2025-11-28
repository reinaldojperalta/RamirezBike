<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="AppRamirezBike.Vista.Catalogo" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">

    <!-- ===================================================== -->
    <!-- JUMBOTRON DE BIENVENIDA -->
    <!-- ===================================================== -->
    <!-- Se mantiene el jumbotron, pero con un ligero ajuste de margen para que se separe mejor de la navbar. -->
    <div class="container mt-4">
      <div class="p-5 bg-light text-dark rounded shadow-sm">
        <h1 class="display-4">Bienvenido a RamirezBike</h1>
        <p class="lead">
          Tu tienda de confianza en bicicletas, refacciones y accesorios.
          Calidad, precio justo y atención personalizada para cada pedal.
        </p>
        <hr class="my-4">
        <p>Envíos a todo el país · Financiación sin intereses · Servicio técnico propio</p>
      </div>
    </div>

    <!-- ===================================================== -->
    <!-- CONTENEDOR PRINCIPAL CON SIDEBAR Y GRID DE PRODUCTOS -->
    <!-- ===================================================== -->
    <!-- La estructura principal ahora tiene dos columnas: una para filtros (sidebar) y otra para el contenido (productos). -->
    <div class="container mt-5">
        <div class="row">
            
            <!-- ===================================================== -->
            <!-- SIDEBAR DE FILTROS -->
            <!-- ===================================================== -->
            <!-- Columna lateral para los filtros. Esto organiza la interfaz y prepara el sitio para futuros filtros (marca, precio, etc.). -->
            <aside class="col-lg-3 mb-4">
                <div class="card">
                    <div class="card-header">
                        <h4 class="mb-0">Filtrar Productos</h4>
                    </div>
                    <div class="card-body">
                        <!-- El DropDownList de categorías ahora está estilizado y dentro de un contexto claro. -->
                        <div class="mb-3">
                            <asp:Label ID="lblFiltro" runat="server" Text="Categoría" CssClass="form-label" AssociatedControlID="ddlCategorias" />
                            <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <!-- Aquí se podrían añadir más filtros en el futuro, como: -->
                        <!-- <div class="mb-3">
                           <label for="precioRange" class="form-label">Rango de Precio</label>
                           <input type="range" class="form-range" id="precioRange">
                        </div> -->
                    </div>
                </div>
            </aside>

            <!-- ===================================================== -->
            <!-- GRID DE PRODUCTOS -->
            <!-- ===================================================== -->
            <!-- Columna principal que contiene el Repeater de productos y la paginación. -->
            <main class="col-lg-9">
                <div class="row g-4"> <!-- 'g-4' añade un espaciado (gutter) consistente entre las columnas -->

                    <asp:Repeater ID="rptProducto" runat="server">
                        <ItemTemplate>
                            <!-- Columna para cada tarjeta de producto. El grid es más responsivo ahora. -->
                            <div class="col-xl-4 col-lg-6 col-md-6">
                                <!-- Tarjeta de producto con un efecto hover sutil y mejor estructura interna. -->
                                <div class="card h-100 shadow-sm transition-hover">
                                    <img src='img/<%# Eval("imgUrl") %>' class="card-img-top img-fluid" alt='<%# Eval("nombre") %>' style="height: 200px; object-fit: cover;">
                                    <div class="card-body d-flex flex-column">
                                        <h5 class="card-title"><%# Eval("nombre") %></h5>
                                        <p class="card-text flex-grow-1"><%# Eval("descripcion") %></p>
                                        <h4 class="card-text text-primary">$<%# Eval("precio") %></h4>
                                        <div class="mt-auto">
                                            <!-- Botón CTA mejorado: "Ver Detalles" es más claro y el estilo es menos agresivo. -->
                                            <a href="Detalle.aspx?id=<%# Eval("idProducto") %>" class="btn btn-outline-primary w-100">Ver Detalles</a>
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
                <!-- La paginación se mantiene funcional, pero se le ha añadido la clase 'mt-4' para separarla del grid de productos. -->
                <nav aria-label="Navegación de Catálogo" class="mt-4">
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
            </main>
        </div>
    </div>

    <!-- Estilo para el efecto hover en las tarjetas -->
    <style>
        .transition-hover {
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        }
        .transition-hover:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.12) !important;
        }
    </style>
</asp:Content>