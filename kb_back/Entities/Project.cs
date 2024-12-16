using System;
using System.Collections.Generic;

namespace kb_back.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Aircraft { get; set; } = null!;

    public string? Department { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly DateBegan { get; set; }

    public DateOnly? DateFinished { get; set; }

    public int? ChiefDesigner { get; set; }

    public virtual Aircraft AircraftNavigation { get; set; } = null!;

    public virtual Employee? ChiefDesignerNavigation { get; set; }

    public virtual Department? DepartmentNavigation { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
