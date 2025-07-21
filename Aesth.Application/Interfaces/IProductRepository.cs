using System.Collections.Generic;
using Aesth.Application.Common;
using Aesth.Domain.Models;

namespace Aesth.Application.Interfaces;

public interface IProductRepository
{
    Product? GetById(long id);
    IEnumerable<Product> GetAll();
    IEnumerable<Product> GetLatestProducts();
    IEnumerable<Product> GetTrendingProducts();
    Task<PageResult<Product>> GetCatalogProductsAsync(int page, int size, string sortBy, string? color, string? type);
    void Create(Product product);
    void Update(Product product);
    void Delete(long id);
}
