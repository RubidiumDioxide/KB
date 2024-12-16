using System;
using System.Collections.Generic;

namespace kb_back.Entities;

public partial class Armament
{
    public string Name { get; set; } = null!;

    public double Caliber { get; set; }

    public double FiringRate { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
}
