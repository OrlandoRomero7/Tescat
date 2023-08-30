using System;
using System.Collections.Generic;

namespace Tescat.Models;

public partial class Email
{
    public Guid UserEmailsId { get; set; }

    public int? IdUser { get; set; }

    public string? Email1 { get; set; }

    public string? Pass1 { get; set; }

    public string? Email2 { get; set; }

    public string? Pass2 { get; set; }

    public string? Email3 { get; set; }

    public string? Pass3 { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
