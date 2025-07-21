using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Products
{
    public class ProductShowDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Images { get; set; } = null!;
    }
}