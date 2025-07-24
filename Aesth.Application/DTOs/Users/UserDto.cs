using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aesth.Application.DTOs.Users
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}