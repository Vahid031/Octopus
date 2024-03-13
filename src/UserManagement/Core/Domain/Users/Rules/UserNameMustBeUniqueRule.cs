using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserNameMustBeUniqueRule : IAsyncBusinessRule
{
    private readonly IUserRepository _userRepository;
    private readonly string _userName;

    public UserNameMustBeUniqueRule(IUserRepository userRepository, string userName)
    {
        _userRepository = userRepository;
        _userName = userName;
    }
    public async Task Validate()
    {
        if (await _userRepository.ExistsWithUserName(_userName))
            throw new UserNameMustBeUniqueException(_userName);
    }
}