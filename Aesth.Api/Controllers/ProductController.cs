using Aesth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Aesth.Domain.Models;
using Aesth.Application.DTOs.Products;
using Aesth.Application.DTOs.Products.Mappers;
using Aesth.Infrastructure.Persistence.Mappers;

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

        var dto = ProductDtoMapper.ToDto(product);
        return Ok(dto);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _getAllProducts.Execute();

        var dtos = products.Select(p => ProductDtoMapper.ToDto(p));
        return Ok(dtos);
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProductCreateDto dto)
    {
        var domain = ProductDtoMapper.ToDomain(dto);
        _createProduct.Execute(domain);
        return CreatedAtAction(nameof(GetById), new { id = domain.Id }, dto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id, [FromBody] ProductDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");

        var domain = ProductDtoMapper.ToDomain(dto);
        _updateProduct.Execute(domain);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        _deleteProduct.Execute(id);
        return Ok(id);
    }
}
