using System;
using System.Collections.Generic;

namespace kb_back.Entities;

public partial class Aircraft
{
    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public byte? Crew { get; set; }

    public double? Weight { get; set; }

    public string? Engine { get; set; }

    public virtual Airframe? Airframe { get; set; }

    public virtual Engine? EngineNavigation { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Armament> Armaments { get; set; } = new List<Armament>();
}
