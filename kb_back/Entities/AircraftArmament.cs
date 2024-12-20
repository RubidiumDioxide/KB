using System;
using System.Collections.Generic;

namespace kb_back;

public partial class AircraftArmament
{
    public string Aircraft { get; set; } = null!;

    public string Armament { get; set; } = null!;

    public byte Quantity { get; set; }

    public virtual Aircraft AircraftNavigation { get; set; } = null!;

    public virtual Armament ArmamentNavigation { get; set; } = null!;
}
