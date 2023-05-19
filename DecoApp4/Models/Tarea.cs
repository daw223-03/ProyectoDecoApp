using System;
using System.Collections.Generic;

namespace DecoApp4.Models;

public partial class Tarea
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int? Cantidad { get; set; }

    public int? Descuento { get; set; }

    public int? Precio { get; set; }

    public int IdFactura { get; set; }

    public int? IdEstado { get; set; }

    public virtual Estado? Estado { get; set; }

    public virtual Factura Factura { get; set; } = null!;
}
