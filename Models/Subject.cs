using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public int YearId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<FacultySubject> FacultySubjects { get; set; } = new List<FacultySubject>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual YearLevel Year { get; set; } = null!;
}
