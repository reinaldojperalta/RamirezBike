let carrito = [];

function cargarCarrito() {
    let guardado = localStorage.getItem("carrito_ramirez");
    if (guardado && guardado !== "null" && guardado !== "[]") {
        carrito = JSON.parse(guardado);
    }
}

// CARGAR AL INICIO (solo una vez)
cargarCarrito();

function guardarCarrito() {
    localStorage.setItem("carrito_ramirez", JSON.stringify(carrito));
}

function añadirAlCarrito(id) {
    for (let i = 0; i < carrito.length; i++) {
        if (carrito[i].idProducto == id) {
            carrito[i].cantidad++;
            guardarCarrito();
            actualizarNumero();
            animarCarrito();
            alert("¡Añadido otra vez!");
            return;
        }
    }
    carrito.push({ idProducto: id, cantidad: 1 });
    guardarCarrito();
    actualizarNumero();
    animarCarrito();
    alert("¡Producto añadido al carrito!");
}

function actualizarNumero() {
    let total = 0;
    for (let i = 0; i < carrito.length; i++) {
        total += carrito[i].cantidad;
    }
    let contador = document.getElementById("cart-count");
    if (contador) {
        contador.innerText = total;
        contador.style.display = total > 0 ? "block" : "none";
    }
}

function irAlCarrito() {
    let datos = localStorage.getItem("carrito_ramirez");
    if (!datos || datos === "[]" || datos === "null" || datos === "") {
        alert("Tu carrito está vacío");
        window.location.href = "Carrito.aspx";
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
    // Solo ejecutamos si estamos en Carrito.aspx
    let divVacio = document.getElementById("divVacio");
    let divConProductos = document.getElementById("divConProductos");
    if (!divVacio && !divConProductos) return; // Salimos si no existen

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
    if (confirm("¿Estás seguro de que quieres vaciar todo el carrito?")) {
        localStorage.removeItem("carrito_ramirez");
        window.location.href = "Carrito.aspx";
    }
}

function modificarCantidad(idProducto, cambio) {
    let datos = localStorage.getItem("carrito_ramirez");
    if (!datos || datos === "[]" || datos === "null") return;

    let temp = JSON.parse(datos);
    for (let i = 0; i < temp.length; i++) {
        if (temp[i].idProducto == idProducto) {
            temp[i].cantidad += cambio;
            if (temp[i].cantidad < 1) {
                if (confirm("¿Quieres eliminar este producto del carrito?")) {
                    temp.splice(i, 1);
                } else {
                    temp[i].cantidad = 1;
                }
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

// === ESTO ES LO QUE FALTABA: EJECUTAR AL CARGAR LA PÁGINA ===
document.addEventListener("DOMContentLoaded", function () {
    actualizarNumero();
    revisarCarrito();
});

// Actualiza si cambian desde otra pestaña
window.addEventListener("storage", function () {
    cargarCarrito();
    actualizarNumero();
    revisarCarrito();
});

// Por si acaso (infalible)
setInterval(function () {
    actualizarNumero();
    revisarCarrito();
}, 1000);

function animarCarrito() {
    let cartIcon = document.querySelector(".bi-cart-plus");
    let cartCount = document.querySelector(".contador-carrito-fijo");

    if (cartIcon && cartCount) {
        cartIcon.classList.remove("animate-icon");
        void cartIcon.offsetWidth;
        cartIcon.classList.add("animate-icon");

        cartCount.classList.remove("animate-badge");
        void cartCount.offsetWidth;
        cartCount.classList.add("animate-badge");

        setTimeout(() => {
            cartIcon.classList.remove("animate-icon");
            cartCount.classList.remove("animate-badge");
        }, 4000);
    }
}