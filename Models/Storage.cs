using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class Storage 
{
    public Guid IdStorage { get; set; }

    public string? Type { get; set; }

    public int? TotalStrge { get; set; }

    public int? AvailableStrge { get; set; }

    public int? UsedStrge { get; set; }

    public string? Model { get; set; }

    public string? State { get; set; }

    public string? UseTime { get; set; }

    public long? NumberRead { get; set; }

    public long? NumberWrite { get; set; }

    public int? ReadSpeed { get; set; }

    public int? WriteSpeed { get; set; }

    public long? Gbw { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}



