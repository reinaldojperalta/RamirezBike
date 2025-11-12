<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/MasterPag1.Master" AutoEventWireup="true" CodeBehind="carrito.aspx.cs" Inherits="AppRamirezBike.Vista.carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody1" runat="server">

    <h1>MI CARRITO</h1>

    <!-- SI NO HAY PRODUCTOS -->
    <div id="carritoVacio">
        <h2>Tu carrito está vacío</h2>
        <p>No has añadido nada aún.</p>
        <a href="Catalogo.aspx">Seguir comprando</a>
    </div>

    <!-- LISTA DE PRODUCTOS -->
    <div id="listaProductos"></div>

    <!-- BOTÓN VACIAR -->
    <div id="botonVaciar" style="text-align: center; margin: 20px; display: none;">
        <button onclick="vaciarCarrito()" style="background: #dc3545; color: white; padding: 12px 30px; border: none; border-radius: 10px; font-size: 18px; cursor: pointer;">
            Vaciar Carrito
       
        </button>
    </div>

    <!-- TOTAL -->
    <div id="resumenTotal" style="text-align: right; margin: 20px; font-size: 24px; font-weight: bold;">
        Total: $<span id="totalPrecio">0</span>
    </div>

    <!-- JS -->
    <script>
        window.onload = function () {
            cargarCarrito();
            actualizarNumero();
            mostrarCarrito();

            // OCULTAR ÍCONO
            let icono = document.querySelector(".carrito-icono");
            if (icono) icono.style.display = "none";
        };

        function mostrarCarrito() {
            let lista = document.getElementById("listaProductos");
            let vacio = document.getElementById("carritoVacio");
            let totalSpan = document.getElementById("totalPrecio");
            let botonVaciar = document.getElementById("botonVaciar");

            lista.innerHTML = "";
            let total = 0;

            if (carrito.length == 0) {
                vacio.style.display = "block";
                document.getElementById("resumenTotal").style.display = "none";
                botonVaciar.style.display = "none";
                return;
            }

            vacio.style.display = "none";
            document.getElementById("resumenTotal").style.display = "block";
            botonVaciar.style.display = "block";

            for (let i = 0; i < carrito.length; i++) {
                let p = carrito[i];
                let subtotal = p.precio * p.cantidad;
                total += subtotal;

                let fila = document.createElement("div");
                fila.innerHTML = `
                    <hr>
                    <img src="${p.imgUrl}" width="70">
                    <strong>${p.nombre}</strong><br>
                    Precio: $${p.precio} | Cantidad: 
                    <button onclick="cambiarCantidad(${i}, -1)">-</button>
                    <strong>${p.cantidad}</strong>
                    <button onclick="cambiarCantidad(${i}, 1)">+</button>
                    | Subtotal: $${subtotal}<br>
                    <button onclick="eliminarDelCarrito(${i})">Eliminar</button>
                `;
                lista.appendChild(fila);
            }

            totalSpan.innerText = total;
        }

        function cambiarCantidad(i, cambio) {
            if (carrito[i].cantidad + cambio > 0) {
                carrito[i].cantidad += cambio;
            } else {
                eliminarDelCarrito(i);
                return;
            }
            guardarCarrito();
            actualizarNumero();
            mostrarCarrito();
        }

        function eliminarDelCarrito(i) {
            carrito.splice(i, 1);
            guardarCarrito();
            actualizarNumero();
            mostrarCarrito();
        }

        function vaciarCarrito() {
            if (confirm("¿Seguro que quieres vaciar el carrito?")) {
                carrito = [];
                guardarCarrito();
                actualizarNumero();
                mostrarCarrito();
            }
        }
    </script>

</asp:Content>
