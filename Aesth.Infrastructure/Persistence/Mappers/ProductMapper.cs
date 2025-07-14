using System;
using System.Collections.Generic;
using System.Linq;
using Aesth.Domain.Models;
using Infrastructure.Persistence.Entities;

namespace Aesth.Infrastructure.Persistence.Mappers
{
    public static class ProductMapper
    {

        public static Product ToDomain(
            product entity,
            IEnumerable<product_image> imageEntities,
            IEnumerable<product_detail> detailEntities,
            IEnumerable<product_size> sizeEntities)
        {
            return new Product
            {
                Id = entity.id,
                Name = entity.name ?? string.Empty,
                Type = entity.type ?? string.Empty,
                Price = entity.price,
                Color = entity.color ?? string.Empty,
                Description = entity.description ?? string.Empty,
                Release = entity.release ?? DateTime.MinValue,
                Availability = entity.availability,
                Views = entity.views ?? 0,

                Images = imageEntities
                    .Where(img => img.product_id == entity.id && img.images != null)
                    .Select(img => img.images!)
                    .ToList(),

                Details = detailEntities
                    .Where(d => d.product_id == entity.id && d.details != null)
                    .Select(d => d.details!)
                    .ToList(),

                Sizes = sizeEntities
                    .Where(s => s.product_id == entity.id && s.sizes != null)
                    .Select(s => s.sizes!)
                    .ToList(),
            };
        }

        public static product ToEntity(Product domain)
        {
            return new product
            {
                id = domain.Id,
                name = domain.Name,
                type = domain.Type,
                price = domain.Price,
                color = domain.Color,
                description = domain.Description,
                release = domain.Release,
                availability = domain.Availability,
                views = domain.Views
            };
        }

        public static List<product_image> ToImageEntities(Product domain)
        {
            return domain.Images.Select(img => new product_image
            {
                product_id = domain.Id,
                images = img
            }).ToList();
        }

        public static List<product_detail> ToDetailEntities(Product domain)
        {
            return domain.Details.Select(d => new product_detail
            {
                product_id = domain.Id,
                details = d
            }).ToList();
        }

        public static List<product_size> ToSizeEntities(Product domain)
        {
            return domain.Sizes.Select(s => new product_size
            {
                product_id = domain.Id,
                sizes = s
            }).ToList();
        }
    }
}
