using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class product_image
{
    public long product_id { get; set; }

    public string? images { get; set; }

    public virtual product product { get; set; } = null!;
}
