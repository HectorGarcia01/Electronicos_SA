const listaProductos = document.querySelector('#lista-carrito tbody');
const listaCompra = document.querySelector("#lista-compra tbody");

//Agregamos producto al carrito
export function comprarProducto(e) {
    e.preventDefault();
    if (e.target.classList.contains('Agregar_Carrito')) {
        const producto = e.target.parentElement.parentElement.parentElement;
        //Enviamos el producto seleccionado
        leerDatosProducto(producto);
    }
}

//Leer los datos del producto seleccionado
function leerDatosProducto(producto) {
    const infoProducto = {
        imagen: producto.querySelector('img').src,
        nombre: producto.querySelector('h2').textContent,
        precio: producto.querySelector('.precio_producto span').textContent,
        stock: producto.querySelector('.cantidad_disponible span').textContent,
        id: producto.querySelector('button').getAttribute('data-id'),
        cantidad: 1
    }

    let productosLS;

    productosLS = obtenerProductosLocalStorage();

    productosLS.forEach(function (productoLS) {
        if (productoLS.id === infoProducto.id) {
            productosLS = productoLS.id;
        }
    });
    
    insertarCarrito(infoProducto);
}

//Mostrar producto seleccionado en el carrito
function insertarCarrito(producto) {
    const row = document.createElement('tr');

    row.innerHTML = `
        <td>
            <img src="${producto.imagen}" width=100>
        </td>
        <td>${producto.nombre}</td>
        <td>Q${producto.precio}</td>
        <td>x${producto.cantidad}</td>
        <td>
            <a href="#" class="borrar-producto fas fa-times-circle" data-id="${producto.id}"></a>
        </td>
    `;
    listaProductos.appendChild(row);
    guardarProductosLocalStorage(producto);

    Swal.fire({
        type: 'success',
        title: 'Excelente',
        text: 'El producto se agregó a tu carrito',
        showConfirmButton: false,
        timer: 1000
    })
}

//Eliminar el producto del carrito
export function eliminarProducto(e) {
    e.preventDefault();
    let producto, productoID;
    if (e.target.classList.contains('borrar-producto')) {
        e.target.parentElement.parentElement.remove();
        producto = e.target.parentElement.parentElement;
        productoID = producto.querySelector('a').getAttribute('data-id');
    }
    eliminarProductoLocalStorage(productoID);
    calcularTotal();
}

//Eliminar todos los productos del carrito
export function vaciarCarrito(e) {
    e.preventDefault();
    while (listaProductos.firstChild) {
        listaProductos.removeChild(listaProductos.firstChild);
    }
    vaciarLocalStorage();

    return false;
}

//Almacenar en el Local Storage
function guardarProductosLocalStorage(producto) {
    let productos;

    //Tomando un valor de un arreglo con datos del Local Storage
    productos = obtenerProductosLocalStorage();

    //Agregando el producto al carrito
    productos.push(producto);

    //Agregando al Local Storage
    localStorage.setItem('productos', JSON.stringify(productos));
}

//Comprobando que existen elementos en el Local Storage
export function obtenerProductosLocalStorage() {
    let productoLS;

    //Comprobar si hay algo en Local Storage
    if (localStorage.getItem('productos') === null) {
        productoLS = [];
    }
    else {
        productoLS = JSON.parse(localStorage.getItem('productos'));
    }
    return productoLS;
}

//Mostrar los Productos guardados en el Local Storage
export function leerLocalStorage() {
    let productosLS;

    productosLS = obtenerProductosLocalStorage();

    productosLS.forEach(function (producto) {
        //Construir plantilla
        const row = document.createElement('tr');

        row.innerHTML = `
            <td>
                <img src="${producto.imagen}" width=100>
            </td>
            <td>${producto.nombre}</td>
            <td>Q${producto.precio}</td>
            <td>x${producto.cantidad}</td>
            <td>
                <a href="#" class="borrar-producto fas fa-times-circle" data-id="${producto.id}"></a>
            </td>
        `;
        listaProductos.appendChild(row);
    });
}

//Mostrando los productos guardados del Local Storage en la vista Facturacion
export function leerLocalStorageCompra() {
    let productosLS;

    productosLS = obtenerProductosLocalStorage();

    productosLS.forEach(function (producto) {

        const row = document.createElement('tr');
        row.innerHTML = `
            <td>
                <img src="${producto.imagen}" width=100>
            </td>
            <td>${producto.nombre}</td>
            <td>Q${producto.precio}</td>
            <td>
                <input type="number" class="form-control cantidad" min="1" max=${producto.stock} value=${producto.cantidad}>
            </td>
            <td id='subtotales'>Q${producto.precio * producto.cantidad}</td>
            <td>
                <a href="#" class="borrar-producto fas fa-times-circle" style="font-size:30px" data-id="${producto.id}"></a>
            </td>
        `;
        listaCompra.appendChild(row);
    });
}

//Eliminar producto por ID del Local Storage
function eliminarProductoLocalStorage(productoID) {
    let productosLS;

    //Obtenemos el arreglo de productos
    productosLS = obtenerProductosLocalStorage();

    //Comparar el id del producto borrado con Local Storage
    productosLS.forEach(function (productoLS, index) {
        if (productoLS.id === productoID) {
            productosLS.splice(index, 1);
        }
    });

    //Añadimos el arreglo actual al Local Storage
    localStorage.setItem('productos', JSON.stringify(productosLS));
}

//Eliminar todos los productos del Local Storage
export function vaciarLocalStorage() {
    localStorage.removeItem("productos");
}

//Procesar pedido
export function procesarPedido(e) {
    e.preventDefault();

    if (obtenerProductosLocalStorage().length === 0) {
        Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'El carrito está vacío, agregar algún producto',
            showConfirmButton: false,
            timer: 2000
        })
    }
    else {
        location.href = "../TiendaElectronicosSA/Facturacion";
    }
}

//Calcular montos
export function calcularTotal() {
    let productosLS;
    let total = 0, IVA = 0, subtotal = 0, puntos=0, descuento=0;

    productosLS = obtenerProductosLocalStorage();

    for (let i = 0; i < productosLS.length; i++) {
        let element = Number(productosLS[i].precio * productosLS[i].cantidad);
        total = total + element;
    }

    IVA = parseFloat(total * 0.12).toFixed(2);
    subtotal = parseFloat(total - IVA).toFixed(2);
    puntos = Math.trunc(total / 8);

    if (puntos >= 3000) {
        document.getElementById('puntoTotal').style.color = "green";
    }
    else {
        document.getElementById('puntoTotal').style.color = "red";
    }

    if (puntos >= 3000 && puntos < 4000) {
        puntos -= 3000;
        descuento = 500;
    }
    else if (puntos >= 4000 && puntos < 5000) {
        puntos -= 4000;
        descuento = 750;
    }
    else if (puntos >= 5000) {
        puntos -= 5000;
        descuento = 1000;
    }

    document.getElementById('subtotal').innerHTML = "Q. " + subtotal;
    document.getElementById('IVA').innerHTML = "Q. " + IVA;
    document.getElementById('Descuento').innerHTML = "Q. " + descuento;
    document.getElementById('total').innerHTML = "Q. " + ((total - descuento).toFixed(2));
    document.getElementById('puntoTotal').innerHTML = puntos;   
}

export function obtenerEvento(e) {
    e.preventDefault();
    let id, cantidad, producto, productosLS;

    if (e.target.classList.contains('cantidad')) {
        producto = e.target.parentElement.parentElement;

        id = producto.querySelector('a').getAttribute('data-id');
        cantidad = producto.querySelector('input').value;

        let actualizarMontos = document.querySelectorAll("#subtotales");
        let actualizarPuntos = document.querySelectorAll("#puntos");
        productosLS = obtenerProductosLocalStorage();

        productosLS.forEach(function (productoLS, index) {
            if (productoLS.id === id) {
                productoLS.cantidad = cantidad;
                actualizarMontos[index].innerHTML = "Q" + Number(cantidad * productosLS[index].precio);
            }
        });
        localStorage.setItem('productos', JSON.stringify(productosLS));
    }
}