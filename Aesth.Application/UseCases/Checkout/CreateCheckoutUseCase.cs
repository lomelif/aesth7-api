using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Products;
using Aesth.Application.Interfaces;

namespace Aesth.Application.UseCases.Checkout
{
    public class CreateCheckoutSessionUseCase
    {
        private readonly ICheckoutService _checkoutService;

        public CreateCheckoutSessionUseCase(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public async Task<string> ExecuteAsync(List<ProductItemDto> items)
        {
            return await _checkoutService.CreateCheckoutSessionAsync(items);
        }
    }
}