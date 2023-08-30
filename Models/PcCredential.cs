using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class PcCredential
{
    public Guid IdPc { get; set; }

    public string? PcUser { get; set; }

    public string? PcPassword { get; set; }

    public string? IdAnydesk { get; set; }

    public string? AnydeskPass { get; set; }

    public string? NetworkUser { get; set; }

    public string? ComunUser { get; set; }

    public string? ComunPass { get; set; }

    public string? MozartRdpUser { get; set; }

    public string? MozartRdpPass { get; set; }

    public string? DarwinVantecUser { get; set; }

    public string? DarwinVantecPass { get; set; }

    public DateTime? LastModif { get; set; }

    public virtual Pc IdPcNavigation { get; set; } = null!;
}
