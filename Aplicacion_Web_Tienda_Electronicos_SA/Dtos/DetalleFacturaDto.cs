using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Dtos
{
    public class DetalleFacturaDto
    {
        public decimal IDDetalle { get; set; }
        public decimal FKFactura { get; set; }
        public decimal FKProducto { get; set; }
        public decimal cantidadCompra { get; set; }
        public decimal precioProducto { get; set; }
        public decimal totalCompra { get; set; }
    }
}