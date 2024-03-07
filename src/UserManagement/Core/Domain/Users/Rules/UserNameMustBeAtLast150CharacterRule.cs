using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserNameMustBeAtLast150CharacterRule(string UserName) : IBussinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName) || UserName.Length > 150)
            throw new UserNameMustBeAtLast150CharacterException(UserName);
    }
}