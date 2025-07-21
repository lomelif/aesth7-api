using Aesth.Domain.Models;
using Aesth.Application.DTOs.Products;

namespace Aesth.Application.DTOs.Products.Mappers;

public static class ProductDtoMapper
{
    public static ProductDto ToDto(Product domain)
    {
        return new ProductDto
        {
            Id = domain.Id,
            Name = domain.Name,
            Type = domain.Type,
            Price = domain.Price,
            Color = domain.Color,
            Description = domain.Description,
            Release = domain.Release,
            Availability = domain.Availability,
            Views = domain.Views,
            Images = domain.Images,
            Details = domain.Details,
            Sizes = domain.Sizes
        };
    }

    public static Product ToDomain(ProductDto dto)
    {
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Type = dto.Type,
            Price = dto.Price,
            Color = dto.Color,
            Description = dto.Description,
            Release = dto.Release,
            Availability = dto.Availability,
            Views = dto.Views,
            Images = dto.Images,
            Details = dto.Details,
            Sizes = dto.Sizes
        };
    }

    public static Product ToDomain(ProductCreateDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Type = dto.Type,
            Price = dto.Price,
            Color = dto.Color,
            Description = dto.Description,
            Release = dto.Release,
            Availability = dto.Availability,
            Images = dto.Images,
            Details = dto.Details,
            Sizes = dto.Sizes,
            Views = 0
        };
    }

    public static ProductShowDto ToProductShowDto(Product domain)
    {
        return new ProductShowDto
        {
            Id = domain.Id,
            Name = domain.Name,
            Price = domain.Price.ToString("F2"),
            Images = domain.Images.FirstOrDefault() ?? ""
        };
    }

    public static ProductCatalogDto ToProductCatalogDto(Product domain)
    {
        return new ProductCatalogDto
        {
            Id = domain.Id,
            Name = domain.Name,
            Type = domain.Type,
            Price = domain.Price,
            Color = domain.Color,
            Release = domain.Release,
            Views = domain.Views,
            Images = domain.Images.FirstOrDefault() ?? ""
        };
    }
}
