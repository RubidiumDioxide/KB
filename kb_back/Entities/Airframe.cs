using System;
using System.Collections.Generic;

namespace kb_back;

public partial class Airframe
{
    public string Name { get; set; } = null!;

    public string? WingProfile { get; set; }

    public double? Length { get; set; }

    public double? Wingspan { get; set; }

    public virtual Aircraft NameNavigation { get; set; } = null!;

    public Airframe() { }

    public Airframe(List<string> Input)
    {
        Name = Input[0];
    }

    public void Set(List<string> Input)
    {
        Name = Input[0];
        WingProfile = Input[1];
        Length = double.Parse(Input[2]);
        Wingspan = double.Parse(Input[3]);
    }
}
