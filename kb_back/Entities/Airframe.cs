using System;
using System.Collections.Generic;

namespace kb_back.Entities;

public partial class Airframe
{
    public string Name { get; set; } = null!;

    public string? WingProfile { get; set; }

    public double? Length { get; set; }

    public double? Wingspan { get; set; }

    public virtual Aircraft NameNavigation { get; set; } = null!;
}
