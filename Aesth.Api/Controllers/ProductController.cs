using Aesth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Aesth.Domain.Models;
using Aesth.Application.DTOs.Products;
using Aesth.Application.DTOs.Products.Mappers;
using Aesth.Infrastructure.Persistence.Mappers;
using Aesth.Application.Common;

namespace Aesth.Api.Controllers;

[ApiController]
[Route("api/Product")]
public class ProductController : ControllerBase
{
    private readonly GetProductById _getProductById;
    private readonly GetAllProducts _getAllProducts;
    private readonly GetLatestProducts _getLatestProducts;
    private readonly GetTrendingProducts _getTrendingProducts;
    private readonly GetCatalogProducts _getCatalogProducts;
    private readonly CreateProduct _createProduct;
    private readonly UpdateProduct _updateProduct;
    private readonly DeleteProduct _deleteProduct;

    public ProductController(
        GetProductById getProductById,
        GetAllProducts getAllProducts,
        GetLatestProducts getLatestProducts,
        GetTrendingProducts getTrendingProducts,
        GetCatalogProducts getCatalogProducts,
        CreateProduct createProduct,
        UpdateProduct updateProduct,
        DeleteProduct deleteProduct)
    {
        _getProductById = getProductById;
        _getAllProducts = getAllProducts;
        _getLatestProducts = getLatestProducts;
        _getTrendingProducts = getTrendingProducts;
        _getCatalogProducts = getCatalogProducts;
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

    [HttpGet("Latest")]
    public IActionResult GetLatestProducts()
    {
        var products = _getLatestProducts.Execute();

        var dtos = products.Select(p => ProductDtoMapper.ToProductShowDto(p));
        return Ok(dtos);
    }

    [HttpGet("Trending")]
    public IActionResult GetTrendingProducts()
    {
        var products = _getTrendingProducts.Execute();

        var dtos = products.Select(p => ProductDtoMapper.ToProductShowDto(p));
        return Ok(dtos);
    }

    [HttpGet("Catalog")]
    public async Task<IActionResult> GetCatalogProducts(
        [FromQuery] int page = 0,
        [FromQuery] int size = 8,
        [FromQuery] string sortBy = "views",
        [FromQuery] string? color = null,
        [FromQuery] string? type = null)
    {
        var result = await _getCatalogProducts.Execute(page, size, sortBy, color, type);

        var dtos = result.Items.Select(p => ProductDtoMapper.ToProductCatalogDto(p));

        return Ok(new PageResult<ProductCatalogDto>
        {
            Items = dtos.ToList(),
            Page = result.Page,
            Size = result.Size,
            TotalItems = result.TotalItems
        });
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
