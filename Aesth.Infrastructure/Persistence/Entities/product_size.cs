using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Infrastructure.Persistence.Entities;

public partial class product_size
{
    [Key]
    public long id { get; set; }
    
    public long product_id { get; set; }

    public string? sizes { get; set; }

    public virtual product product { get; set; } = null!;
}
