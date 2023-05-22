using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Obra
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Direccion { get; set; } = null!;

    public int? IdCliente { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    [StringLength(5)]
    public string Cp { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    [DataType(DataType.Date)]
    public DateTime? FechaFinal { get; set; }

    public int IdEstado { get; set; }

    public int? IdFactura { get; set; }

    public int IdEmpresa { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual Factura? Factura { get; set; }

    public virtual ICollection<Trabajadore> Trabajadores { get; set; } = new List<Trabajadore>();
}
