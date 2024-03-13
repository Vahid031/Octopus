using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<bool> ExistsWithPhoneNumber(PhoneNumber phoneNumber);
    Task<bool> ExistsWithUserName(string userName);
    Task<User> GetByUserName(string userName);
    Task<User> GetByPhoneNumber(string phoneNumber);
}