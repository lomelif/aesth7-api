using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Products;
using Aesth.Application.UseCases.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace Aesth.Api.Controllers
{
    [ApiController]
    [Route("api/Checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly CreateCheckoutSessionUseCase _checkoutUseCase;

        public CheckoutController(CreateCheckoutSessionUseCase checkoutUseCase)
        {
            _checkoutUseCase = checkoutUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] List<ProductItemDto> items)
        {
            try
            {
                var sessionId = await _checkoutUseCase.ExecuteAsync(items);
                return Ok(new { id = sessionId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}