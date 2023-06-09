using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int CategoriaId { get; set; }

    public double PrecioVenta { get; set; }

    public string Estado { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Cantidad { get; set; }

    public byte[]? Imagen1 { get; set; }

    public byte[]? Imagen2 { get; set; }

    public byte[]? Imagen3 { get; set; }

    public byte[]? Imagen4 { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;
}
