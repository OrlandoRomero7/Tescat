using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class MemoryRam
{
    public Guid IdRam { get; set; }

    public string? Model { get; set; }

    public string? TypeRam { get; set; }

    public int? Size { get; set; }

    public string? Speed { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
