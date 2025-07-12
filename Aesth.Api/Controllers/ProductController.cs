

using Aesth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Domain.Model;

namespace Aesth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly GetProductById _getProductById;
    private readonly GetAllProducts _getAllProducts;
    private readonly CreateProduct _createProduct;
    private readonly UpdateProduct _updateProduct;
    private readonly DeleteProduct _deleteProduct;

    public ProductController(
        GetProductById getProductById,
        GetAllProducts getAllProducts,
        CreateProduct createProduct,
        UpdateProduct updateProduct,
        DeleteProduct deleteProduct)
    {
        _getProductById = getProductById;
        _getAllProducts = getAllProducts;
        _createProduct = createProduct;
        _updateProduct = updateProduct;
        _deleteProduct = deleteProduct;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        var product = _getProductById.Execute(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _getAllProducts.Execute();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Product product)
    {
        _createProduct.Execute(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] Product product)
    {
        if (id != product.Id) return BadRequest("ID mismatch");
        _updateProduct.Execute(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _deleteProduct.Execute(id);
        return NoContent();
    }
}
