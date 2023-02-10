using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Dtos
{
    public class FacturaDto
    {
        public decimal IDFactura { get; set; }

        public decimal FKCliente { get; set; }
        public string nitCliente { get; set; }
        public decimal totalFactura { get; set; }

        public List<DetalleFacturaDto> detalleFactura { get; set; }

        public decimal puntoAcumuladoAux { get; set; }
    }
}