using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class ProductoImagen
{
    public int ImagenId { get; set; }

    public int ProductoId { get; set; }

    public byte[] Imagen { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
