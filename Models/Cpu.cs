using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class Cpu
{
    public Guid IdCpu { get; set; }

    public string? Model { get; set; }

    public string? Socket { get; set; }

    public int? Benchmark { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
