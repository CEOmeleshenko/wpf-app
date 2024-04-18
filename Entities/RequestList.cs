using System;
using System.Collections.Generic;

namespace ExamApp.Entities;

public partial class RequestList
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? DeviceName { get; set; }

    public string? Description { get; set; }

    public int? ClientId { get; set; }

    public int Status { get; set; }
}
