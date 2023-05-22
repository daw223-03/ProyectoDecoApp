using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace DecoApp4.Models;

public partial class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Campo obligatorio")]
    public string Password { get; set; } = null!;
}
