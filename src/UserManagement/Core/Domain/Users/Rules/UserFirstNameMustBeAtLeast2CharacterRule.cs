using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserFirstNameMustBeAtLeast2CharacterRule(string FirstName) : IBussinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || FirstName.Length < 2)
            throw new UserFirstNameMustBeAtLeast2CharacterException(FirstName);
    }
}