using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Infrastructure.Persistence.Entities
{
    public partial class order_item
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? size { get; set; }
        public long price { get; set; }
        public int quantity { get; set; }
        public string? image { get; set; }

        public Guid orderid { get; set; }
        public order? Order { get; set; }
    }
}