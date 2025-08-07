using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Products;

namespace Aesth.Application.Interfaces
{
    public interface ICheckoutService
    {
        Task<string> CreateCheckoutSessionAsync(List<ProductItemDto> items);
    }
}