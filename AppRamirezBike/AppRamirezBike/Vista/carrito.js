let carrito = [];

function cargarCarrito() {
    let guardado = localStorage.getItem("carrito_ramirez");
    if (guardado && guardado !== "null" && guardado !== "[]") {
        carrito = JSON.parse(guardado);
    }
}
cargarCarrito();

function guardarCarrito() {
    localStorage.setItem("carrito_ramirez", JSON.stringify(carrito));
}

function capturarDatosYAnadir(idProducto) {
    let txtCantidad = document.getElementById('txtCantidad');
    if (!txtCantidad) return;
    let cantidad = parseInt(txtCantidad.value);
    if (!(cantidad >= 1)) {
        Swal.fire({ icon: 'warning', title: 'Cantidad inválida', text: 'Ingresa mínimo 1', confirmButtonColor: '#198754' });
        return;
    }
    añadirAlCarrito(idProducto, cantidad);
}

function añadirAlCarrito(id, cantidadNueva) {
    for (let i = 0; i < carrito.length; i++) {
        if (carrito[i].idProducto == id) {
            carrito[i].cantidad += cantidadNueva;
            guardarCarrito();
            actualizarNumero();
            animarCarrito();
            Swal.fire({ icon: 'success', title: '¡Añadido otra vez!', toast: true, position: 'top-end', timer: 2000, showConfirmButton: false });
            return;
        }
    }
    carrito.push({ idProducto: id, cantidad: cantidadNueva });
    guardarCarrito();
    actualizarNumero();
    animarCarrito();
    Swal.fire({ icon: 'success', title: '¡Añadido al carrito!', toast: true, position: 'top-end', timer: 2000, showConfirmButton: false });
}

function actualizarNumero() {
    let total = 0;
    for (let i = 0; i < carrito.length; i++) total += carrito[i].cantidad;
    let contador = document.getElementById("cart-count");
    if (contador) {
        contador.innerText = total;
        contador.style.display = total > 0 ? "block" : "none";
    }
}

function irAlCarrito() {
    let datos = localStorage.getItem("carrito_ramirez");
    if (!datos || datos === "[]" || datos === "null" || datos === "") {
        Swal.fire({ icon: 'info', title: 'Carrito vacío', text: 'Agrega productos para comprar', confirmButtonColor: '#198754' });
        return;
    }
    let temp = JSON.parse(datos);
    let cadena = "";
    for (let i = 0; i < temp.length; i++) {
        if (i > 0) cadena += ",";
        cadena += temp[i].idProducto + "-" + temp[i].cantidad;
    }
    window.location.href = "Carrito.aspx?datos=" + cadena;
}

function revisarCarrito() {
    let divVacio = document.getElementById("divVacio");
    let divConProductos = document.getElementById("divConProductos");
    if (!divVacio && !divConProductos) return;
    let guardado = localStorage.getItem("carrito_ramirez");
    if (!guardado || guardado === "[]" || guardado === "null" || guardado === "") {
        divVacio.style.display = "block";
        divConProductos.style.display = "none";
    } else {
        divVacio.style.display = "none";
        divConProductos.style.display = "block";
    }
}

function vaciarCarrito() {
    Swal.fire({
        title: '¿Vaciar carrito?',
        text: 'Se eliminarán todos los productos',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Sí, vaciar',
        cancelButtonText: 'Cancelar'
    }).then((r) => { if (r.isConfirmed) { localStorage.removeItem("carrito_ramirez"); window.location.href = "Carrito.aspx"; } });
}

function modificarCantidad(idProducto, cambio) {
    let datos = localStorage.getItem("carrito_ramirez");
    if (!datos || datos === "[]" || datos === "null") return;
    let temp = JSON.parse(datos);
    for (let i = 0; i < temp.length; i++) {
        if (temp[i].idProducto == idProducto) {
            temp[i].cantidad += cambio;
            if (temp[i].cantidad < 1) {
                Swal.fire({
                    title: '¿Eliminar producto?',
                    text: 'La cantidad llegó a 0',
                    icon: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#dc3545',
                    cancelButtonColor: '#6c757d',
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Mantener (1)'
                }).then((r) => {
                    if (r.isConfirmed) temp.splice(i, 1);
                    else temp[i].cantidad = 1;
                    localStorage.setItem("carrito_ramirez", JSON.stringify(temp));
                    let nuevaCadena = "";
                    for (let j = 0; j < temp.length; j++) {
                        if (j > 0) nuevaCadena += ",";
                        nuevaCadena += temp[j].idProducto + "-" + temp[j].cantidad;
                    }
                    window.location.href = nuevaCadena === "" ? "Carrito.aspx" : "Carrito.aspx?datos=" + nuevaCadena;
                });
                return;
            }
            break;
        }
    }
    localStorage.setItem("carrito_ramirez", JSON.stringify(temp));
    let nuevaCadena = "";
    for (let i = 0; i < temp.length; i++) {
        if (i > 0) nuevaCadena += ",";
        nuevaCadena += temp[i].idProducto + "-" + temp[i].cantidad;
    }
    window.location.href = nuevaCadena === "" ? "Carrito.aspx" : "Carrito.aspx?datos=" + nuevaCadena;
}

document.addEventListener("DOMContentLoaded", function () {
    actualizarNumero();
    revisarCarrito();
});
window.addEventListener("storage", function () {
    cargarCarrito();
    actualizarNumero();
    revisarCarrito();
});
setInterval(function () {
    actualizarNumero();
    revisarCarrito();
}, 1000);

function animarCarrito() {
    let cartIcon = document.querySelector(".bi-cart-plus");
    let cartCount = document.querySelector(".contador-carrito-fijo");
    if (cartIcon && cartCount) {
        cartIcon.classList.add("animate-icon");
        cartCount.classList.add("animate-badge");
        setTimeout(() => {
            cartIcon.classList.remove("animate-icon");
            cartCount.classList.remove("animate-badge");
        }, 4000);
    }
}