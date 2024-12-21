using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Department
{
    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public int Director { get; set; }

    public virtual Employee DirectorNavigation { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public Department() { }

    public Department(List<string> Input)
    {
        Set(Input);
    }

    public Department(Department d)
    {
        Set(d);
    }

    public void Set(List<string> Input)
    {
        Name = Input[0];
        Adress = Input[1];
        Director = int.Parse(Input[2]);
    }

    public void Set(Department d)
    {
        Name = d.Name;
        Adress = d.Adress;
        Director = d.Director;
    }
}
