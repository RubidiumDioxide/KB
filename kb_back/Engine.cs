using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Engine
{
    public string EngineName { get; set; } = null!;

    public string EngineType { get; set; } = null!;

    public double Power { get; set; }

    public double EngineWeight { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
}
