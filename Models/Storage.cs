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

    public string? NumberRead { get; set; }

    public string? NumberWrite { get; set; }

    public int? ReadSpeed { get; set; }

    public int? WriteSpeed { get; set; }

    public string? Gbw { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}



