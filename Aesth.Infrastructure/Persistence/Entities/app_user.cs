using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities;

public partial class app_user
{
    public long id { get; set; }

    public string? email { get; set; }

    public string? last_name { get; set; }

    public string? name { get; set; }

    public string? password { get; set; }

    public string? role { get; set; }

    public virtual ICollection<address> addresses { get; set; } = new List<address>();
}
