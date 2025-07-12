using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class product
{
    public long id { get; set; }

    public bool availability { get; set; }

    public string? color { get; set; }

    public string? description { get; set; }

    public string? name { get; set; }

    public double price { get; set; }

    public DateTime? release { get; set; }

    public string? type { get; set; }

    public long? views { get; set; }
}
