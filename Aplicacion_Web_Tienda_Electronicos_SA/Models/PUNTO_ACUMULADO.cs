//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplicacion_Web_Tienda_Electronicos_SA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PUNTO_ACUMULADO
    {
        public decimal ID_PUNTOS { get; set; }
        public decimal ID_CLIENTE_FK { get; set; }
        public decimal ID_FACTURA_FK { get; set; }
        public decimal PUNTO_CANJEAR { get; set; }
        public Nullable<System.DateTime> FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
    
        public virtual FACTURA FACTURA { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
