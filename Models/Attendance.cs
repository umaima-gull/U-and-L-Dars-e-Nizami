using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
