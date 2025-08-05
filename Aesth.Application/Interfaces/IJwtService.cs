using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Domain.Models;

namespace Aesth.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}