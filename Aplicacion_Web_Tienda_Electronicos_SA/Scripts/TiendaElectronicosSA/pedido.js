import { comprarProducto, eliminarProducto, vaciarCarrito, leerLocalStorage, procesarPedido } from './carrito.js';

const carrito = document.getElementById('carrito');
const productos = document.getElementById('lista-productos');
//const listaProductos = document.querySelector('#lista-carrito tbody');
const btnVaciarCarrito = document.getElementById('vaciar-carrito');
const btnProcesarPedido = document.getElementById('procesar-pedido');

//Se ejecuta cuando agregamos un producto al carrito
productos.addEventListener('click', (e) => { comprarProducto(e) });

//Se ejecuta cuando eliminamos un producto del carrito
carrito.addEventListener('click', (e) => { eliminarProducto(e) });

//Se ejecuta cuando vaciamos el carrito
btnVaciarCarrito.addEventListener('click', (e) => { vaciarCarrito(e) });

//Al cargar la pagina mostramos lo almacenado en Local Storage
document.addEventListener('DOMContentLoaded', leerLocalStorage());

//Enviamos el pedido para ser facturado
btnProcesarPedido.addEventListener('click', (e) => { procesarPedido(e) });