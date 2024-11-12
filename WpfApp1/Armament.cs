using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Armament
{
    public string ArmamentName { get; set; } = null!;

    public double Caliber { get; set; }

    public double FiringRate { get; set; }

    public double ArmamentWeight { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
}
