using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Contrasena { get; set; } = null!;
}
