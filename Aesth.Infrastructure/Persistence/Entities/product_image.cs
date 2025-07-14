using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Infrastructure.Persistence.Entities;

public partial class product_image
{
    [Key]
    public long id { get; set; }
    
    public long product_id { get; set; }

    public string? images { get; set; }

    public virtual product product { get; set; } = null!;
}
