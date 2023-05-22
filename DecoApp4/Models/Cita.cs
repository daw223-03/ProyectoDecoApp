using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DecoApp4.Models;

public partial class Cita
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; }
    [RegularExpression(@"[012]{0,1}[0-9]{1}:[0-6]{1}[0-9]{1}", ErrorMessage = "Formato incorrecto (hh:mm)")]
    public TimeSpan? Hora { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string? Comentario { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
