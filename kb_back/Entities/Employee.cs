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

    public Employee() { }

    public Employee(List<string> Input)
    {
        Set(Input);
    }

    public Employee(Employee e)
    {
        Id = e.Id;
        Surname = e.Surname;
        FirstName = e.FirstName;
        LastName = e.LastName;
        DateOfBirth = e.DateOfBirth;
        Position = e.Position;
        Department = e.Department;
        YearsOfExperience = e.YearsOfExperience;
        CurrentProject = e.CurrentProject;
        Salary = e.Salary;
    }

    public void Set(List<string> Input)
    {
        Surname = Input[1];
        FirstName = Input[2];

        if (Input[3] == "") { LastName = null!; }
        else
        {
            try { LastName = Input[3]; }
            catch { LastName = null!; }
        }
        
        DateOfBirth = DateOnly.Parse(Input[4]);
        Position = Input[5];

        if (Input[6] == "") { Department = null!; }
        else
        {
            try { Department = Input[6]; }
            catch { Department = null!; }
        }

        YearsOfExperience = byte.Parse(Input[7]);
        
        try { CurrentProject = int.Parse(Input[8]); }
        catch { CurrentProject = null!; }
        
        Salary = decimal.Parse(Input[9]);
    }

    public void Set(Employee e)
    {
        Id = e.Id;
        Surname = e.Surname;
        FirstName = e.FirstName;
        LastName = e.LastName;
        DateOfBirth = e.DateOfBirth;
        Position = e.Position;
        Department = e.Department;
        YearsOfExperience = e.YearsOfExperience;
        CurrentProject = e.CurrentProject;
        Salary = e.Salary;
    }
}
