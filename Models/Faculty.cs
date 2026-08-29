using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public int UserId { get; set; }

    public string? Designation { get; set; }

    public string? Qualification { get; set; }

    public int? ExperienceYears { get; set; }

    public string? Specialization { get; set; }

    public string? ContactNo { get; set; }

    public virtual ICollection<FacultySubject> FacultySubjects { get; set; } = new List<FacultySubject>();

    public virtual User User { get; set; } = null!;
}
