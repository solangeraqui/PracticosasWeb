using System;
using System.Collections.Generic;

namespace PracticosasWeb.Models.DB;

public partial class Ventum
{
    public int VentaId { get; set; }

    public int ClienteId { get; set; }

    public double MontoTotal { get; set; }

    public DateTime FechaVenta { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
