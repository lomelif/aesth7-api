using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class address
{
    public long id { get; set; }

    public string? city { get; set; }

    public string? country { get; set; }

    public bool? is_default { get; set; }

    public string? last_name { get; set; }

    public string? name { get; set; }

    public string? neighborhood { get; set; }

    public string? postal_code { get; set; }

    public string? state { get; set; }

    public string? street { get; set; }

    public long user_id { get; set; }

    public virtual app_user user { get; set; } = null!;
}
