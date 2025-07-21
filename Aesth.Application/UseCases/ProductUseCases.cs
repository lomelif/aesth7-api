using Aesth.Application.Common;
using Aesth.Application.Interfaces;
using Aesth.Domain.Models;

namespace Aesth.Application.UseCases;

public class GetProductById
{
    private readonly IProductRepository _repo;
    public GetProductById(IProductRepository repo) => _repo = repo;
    public Product? Execute(long id) => _repo.GetById(id);
}

public class GetAllProducts
{
    private readonly IProductRepository _repo;
    public GetAllProducts(IProductRepository repo) => _repo = repo;
    public IEnumerable<Product> Execute() => _repo.GetAll();
}

public class GetLatestProducts
{
    private readonly IProductRepository _repo;
    public GetLatestProducts(IProductRepository repo) => _repo = repo;
    public IEnumerable<Product> Execute() => _repo.GetLatestProducts();
}

public class GetTrendingProducts
{
    private readonly IProductRepository _repo;
    public GetTrendingProducts(IProductRepository repo) => _repo = repo;
    public IEnumerable<Product> Execute() => _repo.GetTrendingProducts();
}

public class GetCatalogProducts
{
    private readonly IProductRepository _repo;

    public GetCatalogProducts(IProductRepository repo)
    {
        _repo = repo;
    }

    public Task<PageResult<Product>> Execute(
        int page,
        int size,
        string sortBy,
        string? color = null,
        string? type = null)
    {
        return _repo.GetCatalogProductsAsync(page, size, sortBy, color, type);
    }
}

public class CreateProduct
{
    private readonly IProductRepository _repo;
    public CreateProduct(IProductRepository repo) => _repo = repo;
    public void Execute(Product product) => _repo.Create(product);
}

public class UpdateProduct
{
    private readonly IProductRepository _repo;
    public UpdateProduct(IProductRepository repo) => _repo = repo;
    public void Execute(Product product) => _repo.Update(product);
}

public class DeleteProduct
{
    private readonly IProductRepository _repo;
    public DeleteProduct(IProductRepository repo) => _repo = repo;
    public void Execute(long id) => _repo.Delete(id);
}
