using System.Collections.Generic;
using Aesth.Domain.Models;

namespace Aesth.Application.Interfaces;

public interface IProductRepository
{
    Product? GetById(long id);
    IEnumerable<Product> GetAll();
    void Create(Product product);
    void Update(Product product);
    void Delete(long id);
}
