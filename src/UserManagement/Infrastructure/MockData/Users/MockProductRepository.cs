using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Infrastructure.Mock.Users;

internal class MockUserRepository : IUserRepository
{
    private static List<User> _users = new()
    {
        User.Create("alireaz", "+989124777431", "alireza", "sharifi")
    };

    public MockUserRepository()
    {
    }

    public Task<User> GetByUserName(string userName) =>
        Task.FromResult(_users.SingleOrDefault(x => x.UserName.Equals(userName)));

    public Task<User> GetById(UserId id)
    {
        return Task.FromResult(_users.SingleOrDefault(u => u.Id == id));
    }

    public async Task Insert(User user)
    {
        _users.Add(user);
        await Task.CompletedTask;
    }

    public async Task Update(User user)
    {
        var existingUser = _users.SingleOrDefault(u => u.Id == user.Id);
        existingUser = user;
        await Task.CompletedTask;
    }

    public async Task DeleteById(UserId id)
    {
        _users.RemoveAll(u => u.Id == id);
        await Task.CompletedTask;
    }

    public Task<bool> Exisits(UserId id)
    {
        return Task.FromResult<bool>(_users.Any(u => u.Id.Equals(id)));
    }

    public async Task<bool> Exists(UserId id)
    {
        return _users.Any(u => u.Id == id);
    }

    public async Task Commit()
    {
        // This method might be used to save changes to a persistent storage, 
        // but since this is a mock repository, we don't need to do anything here.
        await Task.CompletedTask;
    }
}