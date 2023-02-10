import { leerLocalStorageCompra, eliminarProducto, calcularTotal, obtenerEvento, obtenerProductosLocalStorage } from './carrito.js';

const carrito2 = document.getElementById('carrito');
//const btnProcesarCompra = document.getElementById('procesar-compra');

document.addEventListener('DOMContentLoaded', leerLocalStorageCompra());

//Eliminar productos del carrito
carrito2.addEventListener('click', (e) => { eliminarProducto(e) });

calcularTotal();

carrito2.addEventListener('change', (e) => { obtenerEvento(e) });
carrito2.addEventListener('keyup', (e) => { obtenerEvento(e) });

//Comprobando que existe un cliente logueado en el Local Storage
export function obtenerClienteAuxLocalStorage() {
    let clienteLS;

    //Comprobar si hay algo en Local Storage
    if (localStorage.getItem('sesion_cliente') === null) {
        clienteLS = [];
    }
    else {
        clienteLS = JSON.parse(localStorage.getItem('sesion_cliente'));
    }
    return clienteLS;
}