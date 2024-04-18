using System;
using System.Collections.Generic;

namespace ExamApp.Entities;

public partial class ProblemType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RequestList> RequestLists { get; set; } = new List<RequestList>();
}
