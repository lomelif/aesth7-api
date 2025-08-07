using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.Interfaces;
using Aesth.Domain.Models;
using Aesth.Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.DbContexts;
using Infrastructure.Persistence.Entities;

namespace Aesth.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(Order order)
        {
            var entity = new order
            {
                id = Guid.NewGuid(),
                stripe_session_id = order.StripeSessionId,
                email = order.Email,
                created_at = DateTime.UtcNow,
                address = new order_address
                {
                    line1 = order.Address.Line1,
                    line2 = order.Address.Line2,
                    postal_code = order.Address.PostalCode,
                    city = order.Address.City,
                    country = order.Address.Country,
                },
                items = order.Items.Select(i => new order_item
                {
                    id = Guid.NewGuid(),
                    name = i.Name,
                    size = i.Size,
                    price = i.Price,
                    quantity = i.Quantity,
                    image = i.Image
                }).ToList()
            };

            _context.orders.Add(entity);
            await _context.SaveChangesAsync();
        }
    }

}