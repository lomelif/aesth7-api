using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Products;
using Aesth.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Aesth.Infrastructure.Services
{
    public class StripeCheckoutService : ICheckoutService
    {
        private readonly string _secretKey;

        public StripeCheckoutService(IConfiguration config)
        {
            _secretKey = config["Stripe:SecretKey"]!;
            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<string> CreateCheckoutSessionAsync(List<ProductItemDto> items)
        {
            var lineItems = items.Select(item => new SessionLineItemOptions
            {
                Quantity = item.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "mxn",
                    UnitAmount = item.Price,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"{item.Name} - Size {item.Size}",
                        Images = new List<string> { item.Image }
                    }
                }
            }).ToList();

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/success",
                CancelUrl = "http://localhost:4200/cancel",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "MX" }
                },
                Expand = new List<string> { "line_items" }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Id;
        }
    }
}