using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Products
{
    public class ProductCatalogDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Price { get; set; } 
        public string Color { get; set; } = null!;
        public DateTime Release { get; set; }
        public long Views { get; set; }
        public string Images { get; set; } = null!;
    }
}