using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class FacultySubject
{
    public int Id { get; set; }

    public int FacultyId { get; set; }

    public int SubjectId { get; set; }

    public int ClassYear { get; set; }

    public virtual YearLevel ClassYearNavigation { get; set; } = null!;

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
