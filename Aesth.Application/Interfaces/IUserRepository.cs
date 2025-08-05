using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Domain.Models;

namespace Aesth.Application.Interfaces
{
    public interface IUserRepository
    {
        User? GetById(long id);
        IEnumerable<User> GetAll();
        void Create(User product);
        void Update(User product);
        void Delete(long id);
        Task CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}