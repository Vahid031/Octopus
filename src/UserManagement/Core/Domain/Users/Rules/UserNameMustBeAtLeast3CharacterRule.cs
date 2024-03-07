using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserNameMustBeAtLeast3CharacterRule(string UserName) : IBussinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3)
            throw new UserNameMustBeAtLeast3CharacterException(UserName);
    }
}