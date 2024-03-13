using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Mongo;

internal class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task Commit() => Task.CompletedTask;

    public Task DeleteById(UserId id)
    {
        _users.Remove(_users.First(m => m.Id.Value == id.Value));
        return Task.CompletedTask;
    }

    public Task<bool> ExistsWithPhoneNumber(PhoneNumber phoneNumber)
    {
        return Task.FromResult(_users.Any(m => m.PhoneNumber.Equals(phoneNumber)));
    }

    public Task<bool> ExistsWithUserName(string userName)
    {
        return Task.FromResult(_users.Any(m => m.UserName.Equals(userName)));
    }

    public Task<bool> Exists(UserId id)
    {
        return Task.FromResult(_users.Any(m => m.Id.Value.Equals(id.Value)));
    }

    public Task<User> GetById(UserId id)
    {
        return Task.FromResult(_users.FirstOrDefault(m => m.Id.Value.Equals(id.Value)));
    }

    public Task<User> GetByUserName(string userName)
    {
        return Task.FromResult(_users.FirstOrDefault(m => m.UserName.Equals(userName)));
    }

    public Task<User> GetByPhoneNumber(string phoneNumber)
    {
        return Task.FromResult(_users.Single(u => u.PhoneNumber.Equals(phoneNumber)));
    }

    public Task Insert(User aggregate)
    {
        _users.Add(aggregate);
        return Task.CompletedTask;
    }

    public Task Update(User aggregate)
    {
        _users.Remove(_users.First(m => m.Id.Value == aggregate.Id.Value));
        _users.Add(aggregate);
        return Task.CompletedTask;
    }
}