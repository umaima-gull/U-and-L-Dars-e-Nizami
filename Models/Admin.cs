using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int UserId { get; set; }

    public string? Designation { get; set; }

    public string? ContactNo { get; set; }

    public virtual User User { get; set; } = null!;
}
