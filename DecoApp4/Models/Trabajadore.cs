using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class Trabajadore
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    [StringLength(9)]
    public string Dni { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    [StringLength(9)]
    public string Telefono { get; set; } = null!;

    public string? Direccion { get; set; }
    [RegularExpression(@"[a-z0-9+_.-]+@[a-z]+\.[a-z]{2,3}", ErrorMessage = "Formato incorrecto")]
    public string? Email { get; set; }

    public int? ObraActiva { get; set; }

    public virtual Obra? Obra { get; set; }
}
