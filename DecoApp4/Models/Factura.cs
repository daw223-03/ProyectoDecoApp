using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Factura
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string NombreFactura { get; set; } = null!;

    public int? IdCliente { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }
    public int Iva { get; set; }

    public int IdEstado { get; set; }

    public int IdEmpresa { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Empresa Empresa { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Materiale> Materiales { get; set; } = new List<Materiale>();

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
