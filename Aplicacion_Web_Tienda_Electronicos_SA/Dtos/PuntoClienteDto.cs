using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Dtos
{
    public class PuntoClienteDto
    {
        public decimal IDPunto { get; set; }
        public decimal FKCliente { get; set; }
        public decimal FKFactura { get; set; }
        public decimal puntosCliente { get; set; }
    }
}