using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class VentaDetalle
{
    public int VentaDetalleId { get; set; }

    public int VentaId { get; set; }

    public int ProductoId { get; set; }

    public virtual Ventum Venta { get; set; } = null!;
}
