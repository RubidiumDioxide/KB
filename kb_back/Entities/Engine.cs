using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Engine
{
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public double Power { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();

    public Engine() { }

    public Engine(List<string> Input)
    {
        Name = Input[0];
        Type = Input[1];
        Power = double.Parse(Input[2]); 
        Weight = double.Parse(Input[3]);   
    }

    public void Set(List<string> Input)
    {
        Name = Input[0];
        Type = Input[1];
        Power = double.Parse(Input[2]);
        Weight = double.Parse(Input[3]);
    }
}
