using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Dtos
{
    public class ClienteDto
    {
        public decimal IDCliente { get; set; }
        public decimal FKEstado { get; set; }
        public string nitCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string sexoCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string direccionCliente { get; set; }
        public string correoCliente { get; set; }
        public string passwordCliente { get; set; }
    }
}