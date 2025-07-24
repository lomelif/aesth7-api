using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Domain.Models;

namespace Aesth.Application.DTOs.Users.Mapper;

public static class UserDtoMapper
{
    public static UserDto ToDto(User domain)
    {
        return new UserDto
        {
            Id = domain.Id,
            Name = domain.Name,
            LastName = domain.LastName,
            Email = domain.Email,
            Password = domain.Password,
            Role = domain.Role
        };
    }

    public static User ToDomain(UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Name = dto.Name,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role
        };
    }

    public static User ToDomain(UserCreateDto dto)
    {
        return new User
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role
        };
    }
}
