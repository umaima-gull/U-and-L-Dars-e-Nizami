using System;
using System.Collections.Generic;

namespace Darsenizami.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public int YearLevel { get; set; }

    public int? SubjectId { get; set; }

    public string? PdfLink { get; set; }

    public string? Description { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual YearLevel YearLevelNavigation { get; set; } = null!;
}
