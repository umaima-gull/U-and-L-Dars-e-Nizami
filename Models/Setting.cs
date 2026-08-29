using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Setting
{
    public int SettingId { get; set; }

    public string Key { get; set; } = null!;

    public string? Value { get; set; }
}
