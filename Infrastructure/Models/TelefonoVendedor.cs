//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TelefonoVendedor
    {
        public int TelefonoID { get; set; }
        public int VendedorID { get; set; }
        public string Telefono { get; set; }
    
        public virtual Vendedor Vendedor { get; set; }
    }
}
