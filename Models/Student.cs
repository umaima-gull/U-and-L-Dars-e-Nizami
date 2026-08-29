using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int UserId { get; set; }

    public string? RollNo { get; set; }

    public int YearLevel { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public string? Status { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual User User { get; set; } = null!;

    public virtual YearLevel YearLevelNavigation { get; set; } = null!;
}
