using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserNameMustBeAtLeast3CharacterRule : IBusinessRule
{
    private readonly string _userName;

    public UserNameMustBeAtLeast3CharacterRule(string userName)
    {
        _userName = userName;
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(_userName) || _userName.Length < 3)
            throw new UserNameMustBeAtLeast3CharacterException(_userName);
    }
}