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
    
    public partial class Activos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activos()
        {
            this.Depreciacion = new HashSet<Depreciacion>();
        }
    
        public int ActivoID { get; set; }
        public string NumeroSerie { get; set; }
        public string Modelo { get; set; }
        public System.DateTime FechaCompra { get; set; }
        public System.DateTime FechaVencimientoSeguro { get; set; }
        public System.DateTime FechaVencimientoGarantia { get; set; }
        public decimal CostoColones { get; set; }
        public decimal CostoDolares { get; set; }
        public string Descripcion { get; set; }
        public string CondicionActivo { get; set; }
        public byte[] FotoFactura { get; set; }
        public byte[] FotoActivo { get; set; }
        public int Marca { get; set; }
        public int Vendedor { get; set; }
        public int Asegurador { get; set; }
        public int TipoActivo { get; set; }
        public Nullable<int> ValorActual { get; set; }
        public Nullable<int> VidaUtil { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Depreciacion> Depreciacion { get; set; }
        public virtual Asegurador Asegurador1 { get; set; }
        public virtual Marca Marca1 { get; set; }
        public virtual TipoActivo TipoActivo1 { get; set; }
        public virtual Vendedor Vendedor1 { get; set; }
    }
}
