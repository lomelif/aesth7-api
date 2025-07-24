using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.DTOs.Users;
using Aesth.Application.DTOs.Users.Mapper;
using Aesth.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Aesth.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly GetUserById _getUserById;
        private readonly GetAllUsers _getAllUsers;
        private readonly CreateUser _createUser;
        private readonly UpdateUser _updateUser;
        private readonly DeleteUser _deleteUser;

        public UserController(
            GetUserById getUserById,
            GetAllUsers getAllUsers,
            CreateUser createUser,
            UpdateUser updateUser,
            DeleteUser deleteUser)
        {
            _getUserById = getUserById;
            _getAllUsers = getAllUsers;
            _createUser = createUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var product = _getUserById.Execute(id);
            if (product == null) return NotFound();

            var dto = UserDtoMapper.ToDto(product);
            return Ok(dto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _getAllUsers.Execute();

            var dtos = users.Select(p => UserDtoMapper.ToDto(p));
            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserCreateDto dto)
        {
            var domain = UserDtoMapper.ToDomain(dto);
            _createUser.Execute(domain);
            return CreatedAtAction(nameof(GetById), new { id = domain.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] UserDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var domain = UserDtoMapper.ToDomain(dto);
            _updateUser.Execute(domain);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _deleteUser.Execute(id);
            return Ok(id);
        }
    }
}