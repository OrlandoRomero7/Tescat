using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class UserCredential
{
    public Guid IdCredentials { get; set; }

    public int? IdUser { get; set; }

    public string? PortalUser { get; set; }

    public string? PortalPass { get; set; }

    public string? CasaUser { get; set; }

    public string? CasaPass { get; set; }

    public string? MozartUser { get; set; }

    public string? MozartPass { get; set; }

    public string? DarwinUser { get; set; }

    public string? DarwinPass { get; set; }

    public string? VpnUser { get; set; }

    public string? VpnPass { get; set; }

    public DateTime? LastModif { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
