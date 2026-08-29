using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Admission
{
    public int AdmissionId { get; set; }

    public int? StudentId { get; set; }

    public int FormId { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual AdmissionForms Form { get; set; } = null!;

    public virtual Student? Student { get; set; }
}
