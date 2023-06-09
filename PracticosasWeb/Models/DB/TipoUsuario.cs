using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
