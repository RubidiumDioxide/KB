using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Aircraft { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly DateBegan { get; set; }

    public DateOnly? DateFinished { get; set; }

    public int ChiefDesigner { get; set; }

    public virtual Aircraft AircraftNavigation { get; set; } = null!;

    public virtual Employee ChiefDesignerNavigation { get; set; } = null!;

    public Project() { }

    public Project(List<string> Input)
    {
        Set(Input);
    }

    public Project(Project p)
    {
        Set(p);
    }

    public void Set(List<string> Input)
    {
        Name = Input[1];

        if (Input[2] == "") { Aircraft = null!; }
        else
        {
            try { Aircraft = Input[2]; }
            catch { Aircraft = null!; }
        }

        Status = "начат"; 

        /*if (Input[3] == "") { Status = "начат"; }
        else
        {
            try { Status = Input[3]; }
            catch { Status = "начат"; }
        }

        if (Input[5] == "") { DateBegan = DateOnly.FromDateTime(DateTime.Now); }
        else
        {
            try { DateBegan = DateOnly.Parse(Input[5]); }
            catch { DateBegan = DateOnly.FromDateTime(DateTime.Now); }
        }*/

        DateBegan = DateOnly.FromDateTime(DateTime.Now);
        DateFinished = null!;
        ChiefDesigner = int.Parse(Input[6]);
    }

    public void Set(Project p)
    {
        Id = p.Id;
        Name = p.Name;
        Aircraft = p.Aircraft;
        Status = p.Status;
        DateBegan = p.DateBegan;
        DateFinished = p.DateFinished;
        ChiefDesigner = p.ChiefDesigner;
    }
}
