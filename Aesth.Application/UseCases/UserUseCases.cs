using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aesth.Application.Interfaces;
using Aesth.Domain.Models;

namespace Aesth.Application.UseCases;

public class GetUserById
{
    private readonly IUserRepository _repo;
    public GetUserById(IUserRepository repo) => _repo = repo;
    public User? Execute(long id) => _repo.GetById(id);
}

public class GetAllUsers
{
    private readonly IUserRepository _repo;
    public GetAllUsers(IUserRepository repo) => _repo = repo;
    public IEnumerable<User> Execute() => _repo.GetAll();
}

public class CreateUser
{
    private readonly IUserRepository _repo;
    public CreateUser(IUserRepository repo) => _repo = repo;
    public void Execute(User user) => _repo.Create(user);
}

public class UpdateUser
{
    private readonly IUserRepository _repo;
    public UpdateUser(IUserRepository repo) => _repo = repo;
    public void Execute(User user) => _repo.Update(user);
}

public class DeleteUser
{
    private readonly IUserRepository _repo;
    public DeleteUser(IUserRepository repo) => _repo = repo;
    public void Execute(long id) => _repo.Delete(id);
}