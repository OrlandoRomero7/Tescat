using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class Motherboard
{
    public Guid IdMotherboard { get; set; }

    public string? Model { get; set; }

    public string? FormFactor { get; set; }

    public string? Version { get; set; }

    public string? SocketCpu { get; set; }

    public string? TypeSlot { get; set; }

    public string? RamSlotsNumb { get; set; }

    public string? M2Slot { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
