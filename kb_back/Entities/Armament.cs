using System;
using System.Collections.Generic;
//using kb_back.Entities;

namespace kb_back;

public partial class Armament
{
    public string Name { get; set; } = null!;

    public double Caliber { get; set; }

    public double FiringRate { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<AircraftArmament> AircraftArmaments { get; set; } = new List<AircraftArmament>();
   
    public Armament() { }

    public Armament(List<string> Input)
    {
        Name = Input[0];
        Caliber = double.Parse(Input[1]);
        FiringRate = double.Parse(Input[2]);
        Weight = double.Parse(Input[3]);
    }

    public void Set(List<string> Input)
    {
        Name = Input[0];
        Caliber = double.Parse(Input[1]);
        FiringRate = double.Parse(Input[2]);
        Weight = double.Parse(Input[3]);
    }
}
