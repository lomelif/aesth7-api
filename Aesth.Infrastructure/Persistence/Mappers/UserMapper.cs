using System;
using System.Collections.Generic;
using System.Linq;
using Aesth.Domain.Models;
using Infrastructure.Persistence.Entities;

namespace Aesth.Infrastructure.Persistence.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(
            app_user entity)
        {
            return new User
            {
                Id = entity.id,
                Name = entity.name ?? string.Empty,
                LastName = entity.last_name ?? string.Empty,
                Email = entity.email ?? string.Empty,
                Password = entity.password ?? string.Empty,
                Role = entity.role ?? string.Empty
            };
        }

        public static app_user ToEntity(User domain)
        {
            return new app_user
            {
                id = domain.Id,
                name = domain.Name,
                last_name = domain.LastName,
                email = domain.Email,
                password = domain.Password,
                role = domain.Role
            };
        }
    }
}