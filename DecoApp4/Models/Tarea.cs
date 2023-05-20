using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Tarea
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Descripcion { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    public int? Cantidad { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public int? Descuento { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public int? Precio { get; set; }

    public int IdFactura { get; set; }

    public int? IdEstado { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Factura Factura { get; set; } = null!;
}
