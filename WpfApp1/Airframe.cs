using System;
using System.Collections.Generic;

namespace WpfApp1;

public partial class Airframe
{
    public string Aircraft { get; set; } = null!;

    public string WingProfile { get; set; } = null!;

    public double Length { get; set; }

    public double Wingspan { get; set; }

    public virtual Aircraft AircraftNavigation { get; set; } = null!;
}
