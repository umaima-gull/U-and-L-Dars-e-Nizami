using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int? MarksObtained { get; set; }

    public int? TotalMarks { get; set; }

    public string? Grade { get; set; }

    public string? Term { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
