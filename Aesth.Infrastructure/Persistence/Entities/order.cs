using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Infrastructure.Persistence.Entities
{
    public partial class order
    {
        public Guid id { get; set; }
        public string? stripe_session_id { get; set; }
        public string? email { get; set; }
        public DateTime created_at { get; set; }
        public List<order_item>? items { get; set; }
        public order_address? address { get; set; }
    }
}