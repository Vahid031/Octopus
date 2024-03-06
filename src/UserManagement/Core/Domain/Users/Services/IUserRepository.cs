using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User> GetByUserName(string userName);
}