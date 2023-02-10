using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aplicacion_Web_Tienda_Electronicos_SA.Dtos;
using Aplicacion_Web_Tienda_Electronicos_SA.Models;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Controllers
{
    public class TiendaElectronicosSAController : Controller
    {
        // GET: TiendaElectronicosSA
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar_Cliente()
        {
            return View();
        }

        public ActionResult Iniciar_Sesion()
        {
            return View();
        }

        public ActionResult Productos()
        {
            List<CategoriaDto> listCategorias = new List<CategoriaDto>();
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                listCategorias = (from c in ctx.CATEGORIA
                                  select new CategoriaDto
                                  {
                                      IDCategoria = c.ID_CATEGORIA,
                                      nombreCategoria = c.NOMBRE_CATEGORIA
                                  }).ToList();
            }

            ViewBag.listadoCategorias = listCategorias;

            return View();
        }

        public ActionResult Facturacion()
        {
            return View();
        }

        public ActionResult Finalizar_Compra()
        {
            return View();
        }

        //Para insertar un cliente nuevo
        [HttpPost]
        public ActionResult NuevoCliente(ClienteDto objCliente)
        {
            if (objCliente == null)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                CLIENTE nuevoCliente = new CLIENTE();

                decimal idCliente = ctx.Database.SqlQuery<decimal>("SELECT SW_S_CLIENTE_01.nextval FROM DUAL").SingleOrDefault();

                var validarCorreo = (from c in ctx.CLIENTE
                                    where c.CORREO_CLIENTE == objCliente.correoCliente
                                    select c).FirstOrDefault();

                if(validarCorreo != null)
                {
                    return Json(new { result = "Existe" }, JsonRequestBehavior.AllowGet);
                }

                nuevoCliente.ID_CLIENTE = idCliente;
                nuevoCliente.ID_ESTADO_FK = 2;
                nuevoCliente.NIT_CLIENTE = objCliente.nitCliente;
                nuevoCliente.NOMBRE_CLIENTE = objCliente.nombreCliente;
                nuevoCliente.APELLIDO_CLIENTE = objCliente.apellidoCliente;
                nuevoCliente.SEXO_CLIENTE = objCliente.sexoCliente;
                nuevoCliente.TELEFONO_CLIENTE = objCliente.telefonoCliente;
                nuevoCliente.DIRECCION_CLIENTE = objCliente.direccionCliente;
                nuevoCliente.CORREO_CLIENTE = objCliente.correoCliente;
                nuevoCliente.PASSWORD_CLIENTE = Encrypt.GetSHA256(objCliente.passwordCliente);

                ctx.CLIENTE.Add(nuevoCliente);
                ctx.SaveChanges();
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        //Para validar el inicio de sesión del cliente
        
        [HttpPost]
        public ActionResult ValidarLogin(LoginClienteDto objLogin)
        {
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                string contraseña = Encrypt.GetSHA256(objLogin.passwordLoginCliente);

                var login = (from c in ctx.CLIENTE
                             where c.CORREO_CLIENTE == objLogin.correoLoginCliente && c.PASSWORD_CLIENTE == contraseña
                             select c).FirstOrDefault();

                if(login == null)
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = true}, JsonRequestBehavior.AllowGet);
        }

        //Para obtener datos del cliente
        public JsonResult ObtenerDatosCliente()
        {
            List<ClienteDto> datosCliente = new List<ClienteDto>();

            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                datosCliente = (from dc in ctx.CLIENTE
                                select new ClienteDto { 
                                    IDCliente = dc.ID_CLIENTE,
                                    FKEstado = dc.ID_ESTADO_FK,
                                    nitCliente = dc.NIT_CLIENTE,
                                    nombreCliente = dc.NOMBRE_CLIENTE,
                                    apellidoCliente = dc.APELLIDO_CLIENTE,
                                    telefonoCliente = dc.TELEFONO_CLIENTE,
                                    direccionCliente = dc.DIRECCION_CLIENTE,
                                    correoCliente = dc.CORREO_CLIENTE
                                }).ToList();
            }
            return Json(datosCliente, JsonRequestBehavior.AllowGet);
        }

        //Para actualizar datos del cliente
        [HttpPut]
        public ActionResult ActualizarCliente(ClienteDto objCliente)
        {
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                CLIENTE updateCliente = ctx.CLIENTE.Find(objCliente.IDCliente);


                updateCliente.NIT_CLIENTE = objCliente.nitCliente;
                updateCliente.NOMBRE_CLIENTE = objCliente.nombreCliente;
                updateCliente.APELLIDO_CLIENTE = objCliente.apellidoCliente;
                updateCliente.TELEFONO_CLIENTE = objCliente.telefonoCliente;
                updateCliente.DIRECCION_CLIENTE = objCliente.direccionCliente;

                ctx.Entry(updateCliente).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        //Para obtener datos del producto
        public JsonResult ObtenerDatosProducto()
        {
            List<ProductoDto> listProductos = new List<ProductoDto>();
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                listProductos = (from p in ctx.PRODUCTO
                                 select new ProductoDto
                                 {
                                     IDProducto = p.ID_PRODUCTO,
                                     FKCategoria = p.ID_CATEGORIA_FK,
                                     FKEstado = p.ID_ESTADO_FK,
                                     nombreProducto = p.NOMBRE_PRODUCTO,
                                     precioProducto = p.PRECIO_PROMEDIO,
                                     descripcionProducto = p.DESCRIPCION_PRODUCTO,
                                     stockProducto = p.STOCK,
                                     imagenProducto = p.IMAGEN_PRODUCTO
                                 }).ToList();
            }
            return Json(listProductos, JsonRequestBehavior.AllowGet);
        }

        //Para insertar nueva factura, detalle factura y puntos del cliente
        [HttpPost]
        public ActionResult NuevaFactura(FacturaDto objFactura)
        {
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                //Insertar factura
                FACTURA nuevaFactura = new FACTURA();

                decimal idFactura = ctx.Database.SqlQuery<decimal>("SELECT SW_S_FACTURA_01.nextval FROM DUAL").SingleOrDefault();

                var validarCliente = (from c in ctx.CLIENTE
                                      where c.ID_CLIENTE == objFactura.FKCliente
                                      select c).FirstOrDefault();

                if (validarCliente == null)
                {
                    return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                }

                nuevaFactura.ID_FACTURA = idFactura;
                nuevaFactura.ID_CLIENTE_FK = objFactura.FKCliente;
                nuevaFactura.NIT_CLIENTE = objFactura.nitCliente;
                nuevaFactura.TOTAL_FACUTRA = objFactura.totalFactura;

                ctx.FACTURA.Add(nuevaFactura);
                ctx.SaveChanges();

                //Insertar detalle factura
                foreach (var concept in objFactura.detalleFactura)
                {
                    DETALLE_FACTURA nuevoDetalle = new DETALLE_FACTURA();
                    
                    decimal idDetalle = ctx.Database.SqlQuery<decimal>("SELECT SW_S_DETALLE_FACTURA_01.nextval FROM DUAL").SingleOrDefault();

                    var validarProducto = (from p in ctx.PRODUCTO
                                           where p.ID_PRODUCTO == concept.FKProducto
                                           select p).FirstOrDefault();

                    if (validarProducto == null)
                    {
                        return Json(new { result = false }, JsonRequestBehavior.AllowGet);
                    }

                    nuevoDetalle.ID_DETALLE_FACTURA = idDetalle;
                    nuevoDetalle.ID_FACTURA_FK = idFactura;
                    nuevoDetalle.ID_PRODUCTO_FK = concept.FKProducto;
                    nuevoDetalle.CANTIDAD_COMPRA = concept.cantidadCompra;
                    nuevoDetalle.PRECIO_PRODUCTO = concept.precioProducto;
                    nuevoDetalle.TOTAL_COMPRA = concept.totalCompra;

                    ctx.DETALLE_FACTURA.Add(nuevoDetalle);
                    ctx.SaveChanges();
                }

                //Insertar puntos cliente
                PUNTO_ACUMULADO puntoCliente = new PUNTO_ACUMULADO();

                decimal idPunto = ctx.Database.SqlQuery<decimal>("SELECT SW_S_PUNTO_ACUMULADO_01.nextval FROM DUAL").SingleOrDefault();

                puntoCliente.ID_PUNTOS = idPunto;
                puntoCliente.ID_CLIENTE_FK = objFactura.FKCliente;
                puntoCliente.ID_FACTURA_FK = idFactura;
                puntoCliente.PUNTO_CANJEAR = objFactura.puntoAcumuladoAux;

                ctx.PUNTO_ACUMULADO.Add(puntoCliente);
                ctx.SaveChanges();
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        //Para obtener los puntos de la factura y detalle
        public JsonResult ObtenerFactura()
        {
            List<FacturaDto> listFactura = new List<FacturaDto>();

            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                listFactura = (from f in ctx.FACTURA
                               select new FacturaDto
                               {
                                   IDFactura = f.ID_FACTURA,
                                   FKCliente = f.ID_CLIENTE_FK,
                                   nitCliente = f.NIT_CLIENTE,
                                   totalFactura = f.TOTAL_FACUTRA
                               }).ToList();
            }
            return Json(listFactura, JsonRequestBehavior.AllowGet);
        }

        //Para obtener los puntos de la factura y detalle
        public JsonResult ObtenerDetalle()
        {
            List<DetalleFacturaDto> listDetalle = new List<DetalleFacturaDto>();

            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                listDetalle = (from df in ctx.DETALLE_FACTURA
                               select new DetalleFacturaDto
                               {
                                   IDDetalle = df.ID_DETALLE_FACTURA,
                                   FKFactura = df.ID_FACTURA_FK,
                                   FKProducto = df.ID_PRODUCTO_FK,
                                   cantidadCompra = df.CANTIDAD_COMPRA,
                                   precioProducto = df.PRECIO_PRODUCTO,
                                   totalCompra = df.TOTAL_COMPRA
                               }).ToList();
            }
            return Json(listDetalle, JsonRequestBehavior.AllowGet);
        }

        //Para obtener los puntos del cliente
        public JsonResult ObtenerPuntosCliente()
        {
            List<PuntoClienteDto> listPuntos = new List<PuntoClienteDto>();
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                listPuntos = (from pts in ctx.PUNTO_ACUMULADO
                                 select new PuntoClienteDto
                                 {
                                     IDPunto = pts.ID_PUNTOS,
                                     FKCliente = pts.ID_CLIENTE_FK,
                                     FKFactura = pts.ID_FACTURA_FK,
                                     puntosCliente = pts.PUNTO_CANJEAR
                                 }).ToList();
            }
            return Json(listPuntos, JsonRequestBehavior.AllowGet);
        }

        //Para actualizar los puntos del cliente
        [HttpPut]
        public ActionResult ActualizarPuntos(PuntoClienteDto objPuntos)
        {
            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                PUNTO_ACUMULADO updatePuntos = ctx.PUNTO_ACUMULADO.Find(objPuntos.FKCliente);


                updatePuntos.PUNTO_CANJEAR = objPuntos.puntosCliente;

                ctx.Entry(updatePuntos).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}