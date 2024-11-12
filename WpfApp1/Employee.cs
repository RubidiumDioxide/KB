﻿using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Surname { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string Position { get; set; } = null!;

    public string Department { get; set; } = null!;

    public int YearsOfExperience { get; set; }

    public int? CurrentProject { get; set; }

    public decimal Salary { get; set; }

    public virtual Project? CurrentProjectNavigation { get; set; }

    public virtual Department DepartmentNavigation { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
