using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Events;
using Stripe.Checkout;
using Aesth.Application.DTOs.Checkout;
using Aesth.Application.UseCases.Order;
using System.Text.Json;

namespace Aesth.Api.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;
        private readonly GetOrdersByEmailUseCase _getOrdersByEmailUseCase;
        private readonly string endpointSecret;

        public OrderController(IConfiguration config, CreateOrderUseCase orderUseCase, GetOrdersByEmailUseCase getOrdersByEmailUseCase)
        {
            endpointSecret = config["Stripe:WebhookSecret"]!;
            _createOrderUseCase = orderUseCase;
            _getOrdersByEmailUseCase = getOrdersByEmailUseCase;
        }

        [HttpGet("ByEmail")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email parameter is required.");
            }

            var orders = await _getOrdersByEmailUseCase.GetOrdersByEmailAsync(email);

            if (orders == null || orders.Count == 0)
                return NotFound("No orders found for the given email.");

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var signatureHeader = Request.Headers["Stripe-Signature"];
                var stripeEvent = EventUtility.ConstructEvent(json, signatureHeader, endpointSecret);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;
                    
                    var sessionService = new SessionService();
                    var fullSession = await sessionService.GetAsync(session.Id, new SessionGetOptions
                    {
                        Expand = new List<string> { "line_items.data.price.product" }
                    });

                    var lineItems = fullSession.LineItems.Data;
                    
                    var itemsDto = lineItems.Select(item =>
                    {
                        var product = item.Price?.Product as Product;
                        
                        return new OrderItemDto
                        {
                            Name = product?.Name,
                            Size = product?.Metadata?.ContainsKey("size") == true 
                                ? product.Metadata["size"] : null,
                            Image = product?.Images?.FirstOrDefault(),
                            Price = item.Price?.UnitAmount ?? 0,
                            Quantity = (int)(item.Quantity ?? 0)
                        };
                    }).ToList();

                    var dto = new OrderDto
                    {
                        StripeSessionId = session.Id,
                        Email = session.CustomerDetails?.Email,
                        ShippingAddress = new AddressDto
                        {
                            Country = session.CustomerDetails?.Address?.Country,
                            City = session.CustomerDetails?.Address?.City,
                            Line1 = session.CustomerDetails?.Address?.Line1,
                            Line2 = session.CustomerDetails?.Address?.Line2,
                            PostalCode = session.CustomerDetails?.Address?.PostalCode
                        },
                        Items = itemsDto
                    };

                    await _createOrderUseCase.SaveOrderAsync(dto);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest($"Webhook error: {e.Message}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal error: {e.Message}");
            }
        }
    }
}
