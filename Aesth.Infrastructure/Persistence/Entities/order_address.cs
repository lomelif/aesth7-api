using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Infrastructure.Persistence.Entities
{
    public partial class order_address
    {
        public Guid id { get; set; }
        public string? line1 { get; set; }
        public string? line2 { get; set; }
        public string? postal_code { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
    }
}