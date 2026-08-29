using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class AdmissionForms
{
    public int FormId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Contact { get; set; }

    public string? Address { get; set; }

    public string? PreviousInstitute { get; set; }

    public string? Documents { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}
