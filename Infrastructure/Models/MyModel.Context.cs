﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class activosEntities2 : DbContext
    {
        public activosEntities2()
            : base("name=activosEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Activos> Activos { get; set; }
        public virtual DbSet<Asegurador> Asegurador { get; set; }
        public virtual DbSet<Depreciacion> Depreciacion { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<TipoActivo> TipoActivo { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TelefonoVendedor> TelefonoVendedor { get; set; }
    }
}
