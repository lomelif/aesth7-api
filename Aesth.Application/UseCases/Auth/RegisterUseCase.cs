using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Auth;
using Aesth.Application.Interfaces;
using Aesth.Domain.Models;

namespace Aesth.Application.UseCases.Auth
{
    public class RegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUseCase(IUserRepository userRepository, IJwtService jwtService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> ExecuteAsync(RegisterRequest request)
        {
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new InvalidOperationException("User already exists");

            var user = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Password = _passwordHasher.Hash(request.Password),
                Role = "USER"
            };

            await _userRepository.CreateAsync(user);

            var token = _jwtService.GenerateToken(user!);
            return new AuthResponse { Token = token, UserId = user.Id, Email = user.Email, Role = user.Role };
        }
    }

}