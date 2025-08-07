using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Domain.Models
{
    public class Order
{
    public Guid Id { get; set; }
    public List<OrderItem>? Items { get; set; }
    public Address? Address { get; set; }
    public string? StripeSessionId { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
}