using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Department
{
    public string DepartmentName { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public int Director { get; set; }

    public virtual Employee DirectorNavigation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
