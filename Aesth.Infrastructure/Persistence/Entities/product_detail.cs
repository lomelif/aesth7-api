using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class product_detail
{
    public long product_id { get; set; }

    public string? details { get; set; }

    public virtual product product { get; set; } = null!;
}
