using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Checkout;
using Aesth.Domain.Models;
using Aesth.Application.Interfaces;

namespace Aesth.Application.UseCases.Order
{
    public class CreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task SaveOrderAsync(OrderDto dto)
        {
            var order = new Domain.Models.Order
            {
                Id = Guid.NewGuid(),
                StripeSessionId = dto.StripeSessionId,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                Address = new Address
                {
                    Line1 = dto.ShippingAddress.Line1,
                    Line2 = dto.ShippingAddress.Line2,
                    PostalCode = dto.ShippingAddress.PostalCode,
                    City = dto.ShippingAddress.City,
                    Country = dto.ShippingAddress.Country,
                },
                Items = dto.Items.Select(i => new OrderItem
                {
                    Name = i.Name,
                    Size = i.Size,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Image = i.Image
                }).ToList()
            };

            await _orderRepository.SaveAsync(order);
        }
    }

}