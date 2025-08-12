using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Checkout;
using Aesth.Application.Interfaces;

namespace Aesth.Application.UseCases.Order
{
    public class GetOrdersByEmailUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByEmailUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> GetOrdersByEmailAsync(string email)
        {
            var orders = await _orderRepository.GetOrdersByEmailAsync(email);
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Email = o.Email,
                Date = o.CreatedAt,
                ShippingAddress = o.Address == null ? null : new AddressDto
                {
                    Line1 = o.Address.Line1,
                    Line2 = o.Address.Line2,
                    PostalCode = o.Address.PostalCode,
                    City = o.Address.City,
                    Country = o.Address.Country
                },
                Items = o.Items?.Select(i => new OrderItemDto
                {
                    Name = i.Name,
                    Size = i.Size,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    Image = i.Image
                }).ToList()
            }).ToList();
        }

    }
}