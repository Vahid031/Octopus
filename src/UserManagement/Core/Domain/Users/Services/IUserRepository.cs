using Octopus.Core.Domain.Services;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User> GetByPhoneNumber(string phoneNumber);
}
