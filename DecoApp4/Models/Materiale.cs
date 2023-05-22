using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Materiale
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Nombre { get; set; } = null!;

    public int? Cantidad { get; set; }
    [Range(0, 1000)]
    public decimal? Precio { get; set; }

    public int? IdFactura { get; set; }

    public virtual Factura? Factura { get; set; }
}
