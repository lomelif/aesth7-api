using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Checkout
{
    public class OrderDto
    {
        public List<OrderItemDto>? Items { get; set; }
        public AddressDto? ShippingAddress { get; set; }
        public string? StripeSessionId { get; set; }
        public string? Email { get; set; }
    }
}