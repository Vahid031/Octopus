using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserLastNameMustBeAtLast50CharacterRule(string LastName) : IBussinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(LastName) || LastName.Length > 50)
            throw new UserLastNameMustBeAtLeast2CharacterException(LastName);
    }
}