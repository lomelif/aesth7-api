using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Products
{
    public class ProductItemDto
    {
        public string Name { get; set; } = "";
        public string Image { get; set; } = "";
        public string Size { get; set; } = "";
        public long Price { get; set; }
        public int Quantity { get; set; }
    }
}