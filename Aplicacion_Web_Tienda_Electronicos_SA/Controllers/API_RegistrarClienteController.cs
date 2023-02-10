using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aplicacion_Web_Tienda_Electronicos_SA.Dtos;
using Aplicacion_Web_Tienda_Electronicos_SA.Models;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Controllers
{
    public class API_RegistrarClienteController : ApiController
    {
        [HttpPost, Route("API/RegistrarCliente/NuevoCliente")]
        public IHttpActionResult NuevoCliente([FromBody] ClienteDto objCliente)
        {
            if (objCliente == null)
            {
                return BadRequest("La petición es nula");
            }

            using (Entities_BD_Electronicos_SA ctx = new Entities_BD_Electronicos_SA())
            {
                CLIENTE nuevoCliente = new CLIENTE();

                string passwordEncriptada = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(objCliente.passwordCliente);
                passwordEncriptada = Convert.ToBase64String(encryted);

                nuevoCliente.ID_ESTADO_FK = 2;
                nuevoCliente.NIT_CLIENTE = (objCliente.nitCliente).ToString();
                nuevoCliente.NOMBRE_CLIENTE = objCliente.nombreCliente;
                nuevoCliente.APELLIDO_CLIENTE = objCliente.apellidoCliente;
                nuevoCliente.SEXO_CLIENTE = objCliente.sexoCliente;
                nuevoCliente.TELEFONO_CLIENTE = objCliente.telefonoCliente;
                nuevoCliente.DIRECCION_CLIENTE = objCliente.direccionCliente;
                nuevoCliente.CORREO_CLIENTE = objCliente.correoCliente;
                nuevoCliente.PASSWORD_CLIENTE = passwordEncriptada;

                ctx.CLIENTE.Add(nuevoCliente);
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}
