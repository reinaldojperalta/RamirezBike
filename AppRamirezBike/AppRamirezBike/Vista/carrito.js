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


function añadirAlCarrito(id) {
    for (let i = 0; i < carrito.length; i++) {
        if (carrito[i].idProducto == id) {
            carrito[i].cantidad++;
            guardarCarrito();
            actualizarNumero();
            alert(`¡Añadido otra vez! `);
            return;
        }
    }

    carrito.push({
        idProducto: id,
        cantidad: 1
    });

    guardarCarrito();
    actualizarNumero();
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
        if (total > 0) {
            contador.style.display = "block";
        } else {
            contador.style.display = "none";
        }
    }
}
function irAlCarrito() {
    let datos = localStorage.getItem("carrito_ramirez");

    // Si está vacío o no existe
    if (!datos || datos === "[]" || datos === "null" || datos === "") {
        alert("Tu carrito está vacío");
        window.location.href = "Carrito.aspx";
        return;
    }

    // Convertimos el array a formato: id-cantidad,id-cantidad
    let carrito = JSON.parse(datos);
    let cadena = "";

    for (let i = 0; i < carrito.length; i++) {
        if (i > 0) {
            cadena += ",";
        }

        // 2. Construcción de la pareja ID-CANTIDAD
        cadena += carrito[i].idProducto + "-" + carrito[i].cantidad;
    }

    // Redirigimos con los datos en la URL
    window.location.href = "Carrito.aspx?datos=" + cadena;
}

// 1. PRIMERO: función para revisar si está vacío o no
function revisarCarrito() {
    let guardado = localStorage.getItem("carrito_ramirez");
    if (!guardado || guardado === "[]" || guardado === "null") {
        document.getElementById("divVacio").style.display = "block";
        document.getElementById("divConProductos").style.display = "none";
    } else {
        document.getElementById("divVacio").style.display = "none";
        document.getElementById("divConProductos").style.display = "block";
    }
}

// 2. SEGUNDO: función para vaciar (aquí sí puede llamar a revisarCarrito)
function vaciarCarrito() {
    if (confirm("¿Estás seguro de que quieres vaciar todo el carrito?")) {
        // 1. Borramos del localStorage
        localStorage.removeItem("carrito_ramirez");

        // 2. Redirigimos a Carrito.aspx limpio
        window.location.href = "Carrito.aspx";
    }
}
function modificarCantidad(idProducto, cambio) {
    let datos = localStorage.getItem("carrito_ramirez");

    if (!datos || datos === "[]" || datos === "null") {
        return;
    }

    let carrito = JSON.parse(datos);
    let encontrado = false;

    // Buscamos el producto y modificamos la cantidad
    for (let i = 0; i < carrito.length; i++) {
        if (carrito[i].idProducto == idProducto) {
            carrito[i].cantidad += cambio;

            // No permitimos cantidad menor a 1
            if (carrito[i].cantidad < 1) {
                if (confirm("¿Quieres eliminar este producto del carrito?")) {
                    carrito.splice(i, 1); // lo quitamos
                } else {
                    carrito[i].cantidad = 1; // lo dejamos en 1
                }
            }
            encontrado = true;
            break;
        }
    }

    // Si no lo encontró (raro, pero por seguridad)
    if (!encontrado) {
        return;
    }

    // Guardamos de nuevo
    localStorage.setItem("carrito_ramirez", JSON.stringify(carrito));

    // Reconstruimos la URL con el formato id-cantidad,id-cantidad
    let nuevaCadena = "";
    for (let i = 0; i < carrito.length; i++) {
        if (i > 0) nuevaCadena += ",";
        nuevaCadena += carrito[i].idProducto + "-" + carrito[i].cantidad;
    }

    // Redirigimos con los nuevos datos
    if (nuevaCadena === "") {
        window.location.href = "Carrito.aspx";
    } else {
        window.location.href = "Carrito.aspx?datos=" + nuevaCadena;
    }
}


// 3. AL FINAL: ejecutamos al cargar la página
revisarCarrito();


actualizarNumero();