using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Employee
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Position { get; set; } = null!;

    public string? Department { get; set; }

    public byte YearsOfExperience { get; set; }

    public int? CurrentProject { get; set; }

    public decimal Salary { get; set; }

    public virtual Project? CurrentProjectNavigation { get; set; }

    public virtual Department? DepartmentNavigation { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
