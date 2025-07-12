using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class product_size
{
    public long product_id { get; set; }

    public string? sizes { get; set; }

    public virtual product product { get; set; } = null!;
}
