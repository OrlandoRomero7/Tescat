using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class PowerSupply
{
    public Guid IdPsu { get; set; }

    public string? Model { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
