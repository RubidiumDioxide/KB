using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Aircraft
{
    public string AircraftType { get; set; } = null!;
    public string AircraftName { get; set; } = null!;


    public int? Crew { get; set; }

    public double? AircraftWeight { get; set; }

    public string? Engine { get; set; }

    public virtual Airframe? Airframe { get; set; }

    public virtual Engine? EngineNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Armament> Armaments { get; set; } = new List<Armament>();
}
