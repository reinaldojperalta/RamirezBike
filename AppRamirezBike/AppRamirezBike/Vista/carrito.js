let carrito = [];

function cargarCarrito() {
    let guardado = localStorage.getItem("carrito_ramirez");
    if (guardado) {
        carrito = JSON.parse(guardado);
    }
}
cargarCarrito();

function guardarCarrito() {
    localStorage.setItem("carrito_ramirez", JSON.stringify(carrito));
}

function añadirAlCarrito(id, nombre, precio, imagen) {
    for (let i = 0; i < carrito.length; i++) {
        if (carrito[i].idProducto == id) {
            carrito[i].cantidad++;
            guardarCarrito();
            actualizarNumero();
            alert("¡AÑADIDO OTRA VEZ!");
            return;
        }
    }
    carrito.push({
        idProducto: id,
        nombre: nombre,
        precio: precio,
        imgUrl: imagen,
        cantidad: 1
    });
    guardarCarrito();
    actualizarNumero();
    alert("¡AÑADIDO AL CARRITO!");
}

function actualizarNumero() {
    let total = 0;
    for (let i = 0; i < carrito.length; i++) {
        total += carrito[i].cantidad;
    }
    let span = document.getElementById("cart-count");
    if (span) {
        span.innerText = total;

      
        if (total > 0) {
            span.style.display = "block"; // Mostrar el contador
        } else {
            span.style.display = "none";  // Ocultar el contador
        }
        
    }
}

actualizarNumero();