using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    // Nota
    // Debe colocar este attribute en la clase bodega
    // [MetadataType(typeof(BodegaMetadata))]
    internal partial class ActivosMetadata
    {
        [Display(Name = "Código")]
        public int ActivoID { get; set; }



        [Display(Name = "Número de serie")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Número de serie requerido")]
        public string NumeroSerie { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Modelo requerido")]
        public string Modelo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de compra")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaCompra { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiración de seguro")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaVencimientoSeguro { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiración de garantía")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaVencimientoGarantia { get; set; }

        [Display(Name = "Costo en Colón CR")]
        //[MaxLength(12)]
        //[MinLength(1)]
        public decimal CostoColones { get; set; }

        [Display(Name = "Costo en dólar")]
        public decimal CostoDolares { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Descripción requerida")]
        public string Descripcion { get; set; }

        [Display(Name = "Condición")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Condición requerida")]
        public string CondicionActivo { get; set; }

        [Display(Name = "Factura")]
        public byte[] FotoFactura { get; set; }

        [Display(Name = "Activo")]
        public byte[] FotoActivo { get; set; }

        [Display(Name = "Marca")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione una Marca")]
        public int Marca { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione un Vendedor")]
        public int Vendedor { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Selecione un Asegurador")]
        public int Asegurador { get; set; }

        [Display(Name = "Tipo de activo")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Tipo de activo requerido")]
        public int TipoActivo { get; set; }

        [Display(Name = "Valor Actual")]
        public Nullable<int> ValorActual { get; set; }

        [Display(Name = "Vida útil")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Vida útil requerido")]
        public Nullable<int> VidaUtil { get; set; }


    }


    internal partial class AseguradorMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int AseguradorID { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de asegurador requerido")]

        [Display(Name = "Nombre de asegurador")]
        public string Descripcion { get; set; }


    }

    internal partial class DepreciacionMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int DepreciacionID { get; set; }

        [Display(Name = "Fecha de depreciación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fecha de depreciación requerida")]
        public System.DateTime Fecha { get; set; }

        [Display(Name = "Valor en colones")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Valor en colones requerida")]
        public string Valor { get; set; }

        [Display(Name = "Activo")]
        public Nullable<int> Activo { get; set; }

        [Display(Name = "Activo")]
        public virtual Activos Activos { get; set; }
    }


    internal partial class MarcaMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int MarcaID { get; set; }

        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Marca requerida")]
        public string Descripcion { get; set; }
    }

    [MetadataType(typeof(MarcaMetadata))]

    internal partial class RolMetadata
    {
        [Display(Name = "Código")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int IdRol { get; set; }

        [Display(Name = "Rol")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rol requerido")]
        public string Descripcion { get; set; }
    }


    
    internal partial class TelefonoVendedorMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int TelefonoID { get; set; }

        [Display(Name = "Vendedor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vendedor requerido")]
        public int VendedorID { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Teléfono requerido")]

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "El formato ingresado del Teléfono no es correcto")]
        public string Telefono { get; set; }

    }

    [MetadataType(typeof(MarcaMetadata))]

    internal partial class TipoActivoMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Código requerido")]
        public int TipoActivoID { get; set; }

        [Display(Name = "Tipo de activo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tipo de activo requerido")]
        public string Descripcion { get; set; }
    }


   

    internal partial class UsuarioMetadata 
    {

        [Display(Name = "Usuario ID")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Usuario requerido")]
        public string Login { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessage = "Apellidos requerido")]
        public string Apellidos { get; set; }

        [Display(Name = "Contraseña")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Contraseña requerida")]
        public string Password { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessage = "Estado requerido")]
        public string Estado { get; set; }

        [Display(Name = "Rol")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Rol requerido")]
        public int IdRol { get; set; }
    }


   

    internal partial class VendedorMetadata
    {
        [Display(Name = "Código")]
        public int VendedorID { get; set; }

        [Display(Name = "Cédula jurídica")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cédula jurídica requerida")]
        public string CedulaJuridica { get; set; }

        [Display(Name = "Nombre de establecimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de establecimiento requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Correo electrónico requerido")]
        public string CorreoElectronico { get; set; }

        [Display(Name = "Nombre de contacto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nombre de contacto  requerido")]
        public string NombreContacto { get; set; }

    }


}
