using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Checkout
{
    public class AddressDto
    {
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}