using Aesth.Application.Interfaces;
using Domain.Model;

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
