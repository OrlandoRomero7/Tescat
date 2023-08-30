using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class Storage
{
    public Guid IdStorage { get; set; }

    public string? Type { get; set; }

    public string? TotalStrge { get; set; }

    public string? AvailableStrge { get; set; }

    public string? UsedStrge { get; set; }

    public string? Model { get; set; }

    public string? State { get; set; }

    public string? UseTime { get; set; }

    public string? NumberRead { get; set; }

    public string? NumberWrite { get; set; }

    public string? ReadSpeed { get; set; }

    public string? WriteSpeed { get; set; }

    public string? Gbw { get; set; }

    public Guid? IdPc { get; set; }

    public virtual Pc? IdPcNavigation { get; set; }
}
