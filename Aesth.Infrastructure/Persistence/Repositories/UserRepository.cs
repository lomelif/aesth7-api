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
using Microsoft.AspNetCore.Identity;

namespace Aesth.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetById(long id)
        {
            var userEntity = _context.app_users.Find(id);
            if (userEntity == null) return null;

            return UserMapper.ToDomain(userEntity);
        }

        public IEnumerable<User> GetAll()
        {
            var users = _context.app_users.ToList();

            return users.Select(p => UserMapper.ToDomain(p));
        }

        public void Create(User domainUser)
        {
            var entity = UserMapper.ToEntity(domainUser);
            _context.app_users.Add(entity);
            _context.SaveChanges();
        }

        public void Update(User domainUser)
        {
            var existingEntity = _context.app_users.Find(domainUser.Id);
            if (existingEntity == null) throw new KeyNotFoundException("User no encontrado");

            if (!string.IsNullOrWhiteSpace(domainUser.Name))
                existingEntity.name = domainUser.Name;

            if (!string.IsNullOrWhiteSpace(domainUser.LastName))
                existingEntity.last_name = domainUser.LastName;

            if (!string.IsNullOrWhiteSpace(domainUser.Email))
                existingEntity.email = domainUser.Email;

            if (!string.IsNullOrWhiteSpace(domainUser.Role))
                existingEntity.role = domainUser.Role;

            if (!string.IsNullOrWhiteSpace(domainUser.Password))
                existingEntity.password = domainUser.Password;

            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.app_users.Find(id);
            if (entity == null) throw new KeyNotFoundException("User no encontrado");

            _context.app_users.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var entity = await _context.app_users.FirstOrDefaultAsync(u => u.email == email);
            if (entity == null) return null;

            return new User { Id = entity.id, Email = entity.email!, Password = entity.password!, Role = entity.role! };
        }

        public async Task CreateAsync(User domainUser)
        {
            var entity = UserMapper.ToEntity(domainUser);
            _context.app_users.Add(entity);
            await _context.SaveChangesAsync();

            domainUser.Id = entity.id;
        }
    }
}