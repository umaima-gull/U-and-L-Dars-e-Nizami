using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string? Message { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? DateSent { get; set; }

    public virtual User User { get; set; } = null!;
}
