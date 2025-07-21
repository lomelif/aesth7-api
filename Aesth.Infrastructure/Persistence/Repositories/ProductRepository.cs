using System.Collections.Generic;
using System.Linq;
using Aesth.Application.Interfaces;
using Aesth.Infrastructure.Persistence.Mappers;
using Aesth.Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Aesth.Application.Common;

namespace Aesth.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public Product? GetById(long id)
    {
        var productEntity = _context.products.Find(id);
        if (productEntity == null) return null;

        var images = _context.product_images.Where(i => i.product_id == id).ToList();
        var details = _context.product_details.Where(d => d.product_id == id).ToList();
        var sizes = _context.product_sizes.Where(s => s.product_id == id).ToList();

        return ProductMapper.ToDomain(productEntity, images, details, sizes);
    }

    public IEnumerable<Product> GetAll()
    {
        var products = _context.products.ToList();

        var images = _context.product_images.ToList();
        var details = _context.product_details.ToList();
        var sizes = _context.product_sizes.ToList();

        return products.Select(p => ProductMapper.ToDomain(
            p,
            images.Where(i => i.product_id == p.id),
            details.Where(d => d.product_id == p.id),
            sizes.Where(s => s.product_id == p.id)
        ));
    }

    public IEnumerable<Product> GetLatestProducts()
    {
        var products = _context.products
        .OrderByDescending(p => p.release)
        .ToList();

        var images = _context.product_images.ToList();
        var details = _context.product_details.ToList();
        var sizes = _context.product_sizes.ToList();

        return products.Select(p => ProductMapper.ToDomain(
            p,
            images.Where(i => i.product_id == p.id),
            details.Where(d => d.product_id == p.id),
            sizes.Where(s => s.product_id == p.id)
        ));
    }

    public IEnumerable<Product> GetTrendingProducts()
    {
        var products = _context.products
        .OrderByDescending(p => p.views)
        .ToList();

        var images = _context.product_images.ToList();
        var details = _context.product_details.ToList();
        var sizes = _context.product_sizes.ToList();

        return products.Select(p => ProductMapper.ToDomain(
            p,
            images.Where(i => i.product_id == p.id),
            details.Where(d => d.product_id == p.id),
            sizes.Where(s => s.product_id == p.id)
        ));
    }

    public async Task<PageResult<Product>> GetCatalogProductsAsync(int page, int size, string sortBy, string? color, string? type)
    {
        var query = _context.products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(color))
            query = query.Where(p => p.color == color);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(p => p.type == type);

        query = sortBy switch
        {
            "price_asc" => query.OrderBy(p => p.price),
            "price_desc" => query.OrderByDescending(p => p.price),
            "name_asc" => query.OrderBy(p => p.name),
            "name_desc" => query.OrderByDescending(p => p.name),
            "release" => query.OrderByDescending(p => p.release),
            _ => query.OrderByDescending(p => p.views)
        };

        var totalItems = await query.CountAsync();

        var products = await query
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        var productIds = products.Select(p => p.id).ToList();

        var images = await _context.product_images
            .Where(i => productIds.Contains(i.product_id))
            .ToListAsync();

        var details = await _context.product_details
            .Where(d => productIds.Contains(d.product_id))
            .ToListAsync();

        var sizes = await _context.product_sizes
            .Where(s => productIds.Contains(s.product_id))
            .ToListAsync();

        var domainProducts = products.Select(p => ProductMapper.ToDomain(
            p,
            images.Where(i => i.product_id == p.id),
            details.Where(d => d.product_id == p.id),
            sizes.Where(s => s.product_id == p.id)
        )).ToList();

        return new PageResult<Product>
        {
            Items = domainProducts,
            Page = page,
            Size = size,
            TotalItems = totalItems
        };
    }

    public void Create(Product domainProduct)
    {
        var entity = ProductMapper.ToEntity(domainProduct);
        _context.products.Add(entity);
        _context.SaveChanges();

        var productId = entity.id;

        var images = ProductMapper.ToImageEntities(domainProduct);
        var details = ProductMapper.ToDetailEntities(domainProduct);
        var sizes = ProductMapper.ToSizeEntities(domainProduct);

        images.ForEach(i => i.product_id = productId);
        details.ForEach(d => d.product_id = productId);
        sizes.ForEach(s => s.product_id = productId);

        _context.product_images.AddRange(images);
        _context.product_details.AddRange(details);
        _context.product_sizes.AddRange(sizes);

        _context.SaveChanges();
    }

    public void Update(Product domainProduct)
    {
        var existingEntity = _context.products.Find(domainProduct.Id);
        if (existingEntity == null) throw new KeyNotFoundException("Producto no encontrado");

        existingEntity.name = domainProduct.Name;
        existingEntity.type = domainProduct.Type;
        existingEntity.price = domainProduct.Price;
        existingEntity.color = domainProduct.Color;
        existingEntity.description = domainProduct.Description;
        existingEntity.release = domainProduct.Release;
        existingEntity.availability = domainProduct.Availability;
        existingEntity.views = domainProduct.Views;

        _context.SaveChanges();

        var productId = existingEntity.id;

        var existingImages = _context.product_images.Where(i => i.product_id == productId);
        _context.product_images.RemoveRange(existingImages);
        var newImages = ProductMapper.ToImageEntities(domainProduct);
        newImages.ForEach(i => i.product_id = productId);
        _context.product_images.AddRange(newImages);

        var existingDetails = _context.product_details.Where(d => d.product_id == productId);
        _context.product_details.RemoveRange(existingDetails);
        var newDetails = ProductMapper.ToDetailEntities(domainProduct);
        newDetails.ForEach(d => d.product_id = productId);
        _context.product_details.AddRange(newDetails);

        var existingSizes = _context.product_sizes.Where(s => s.product_id == productId);
        _context.product_sizes.RemoveRange(existingSizes);
        var newSizes = ProductMapper.ToSizeEntities(domainProduct);
        newSizes.ForEach(s => s.product_id = productId);
        _context.product_sizes.AddRange(newSizes);

        _context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = _context.products.Find(id);
        if (entity == null) throw new KeyNotFoundException("Producto no encontrado");

        var images = _context.product_images.Where(i => i.product_id == id);
        var details = _context.product_details.Where(d => d.product_id == id);
        var sizes = _context.product_sizes.Where(s => s.product_id == id);

        _context.product_images.RemoveRange(images);
        _context.product_details.RemoveRange(details);
        _context.product_sizes.RemoveRange(sizes);

        _context.products.Remove(entity);

        _context.SaveChanges();
    }
}
