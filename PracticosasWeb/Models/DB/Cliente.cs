using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Distrito { get; set; } = null!;

    public string Departamento { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
