using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Aircraft
{
    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public byte? Crew { get; set; }

    public double? Weight { get; set; }

    public string? Engine { get; set; }

    public virtual ICollection<AircraftArmament> AircraftArmaments { get; set; } = new List<AircraftArmament>();

    public virtual Airframe? Airframe { get; set; }

    public virtual Engine? EngineNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public Aircraft() { }

    public Aircraft(List<string> Input)
    {
        Set(Input);
    }

    public void Set(List<string> Input)
    {
        Name = Input[0];
        Type = Input[1];
        Crew = byte.Parse(Input[2]);
        Weight = double.Parse(Input[3]);
        Engine = Input[4];
    }
}
