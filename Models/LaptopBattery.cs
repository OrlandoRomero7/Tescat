using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class LaptopBattery
{
    public Guid IdBattery { get; set; }

    public string? Model { get; set; }

    public string? Capacity { get; set; }

    public DateTime? FabDate { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
