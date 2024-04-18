using System;
using System.Collections.Generic;

namespace ExamApp.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<RequestList> RequestListClients { get; set; } = new List<RequestList>();

    public virtual ICollection<RequestList> RequestListWorkers { get; set; } = new List<RequestList>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
