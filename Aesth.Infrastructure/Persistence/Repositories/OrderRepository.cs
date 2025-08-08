using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.Interfaces;
using Aesth.Domain.Models;
using Aesth.Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.DbContexts;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Order>> GetOrdersByEmailAsync(string email)
        {
            var entities = await _context.orders
                .Include(o => o.items)
                .Include(o => o.address)  
                .Where(o => o.email == email)
                .ToListAsync();

            return entities.Select(o => new Order
            {
                Id = o.id,
                StripeSessionId = o.stripe_session_id,
                Email = o.email,
                CreatedAt = o.created_at,
                Address = o.address == null ? null : new Address
                {
                    Line1 = o.address.line1,
                    Line2 = o.address.line2,
                    PostalCode = o.address.postal_code,
                    City = o.address.city,
                    Country = o.address.country
                },
                Items = o.items?.Select(i => new OrderItem
                {
                    Name = i.name,
                    Size = i.size,
                    Price = i.price,
                    Quantity = i.quantity,
                    Image = i.image
                }).ToList()
            }).ToList();
        }

    }

}