using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Dtos
{
    public class ProductoDto
    {
        public decimal IDProducto { get; set; }
        public decimal FKCategoria { get; set; }
        public decimal FKEstado { get; set; }
        public string nombreProducto { get; set; }
        public decimal precioProducto { get; set; }
        public string descripcionProducto { get; set; }
        public decimal stockProducto { get; set; }
        public string imagenProducto { get; set; }

    }
}