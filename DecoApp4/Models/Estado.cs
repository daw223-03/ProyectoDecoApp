using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Estado
{
    [Required(ErrorMessage = "Campo obligatorio")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Obra> Obras { get; set; } = new List<Obra>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
