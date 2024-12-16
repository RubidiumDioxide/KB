using System;
using System.Collections.Generic;

namespace kb_back.Entities;

public partial class Engine
{
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public double Power { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
}
