using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserLastNameMustBeAtLeast2CharacterRule(string LastName) : IBusinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(LastName) || LastName.Length < 2)
            throw new UserLastNameMustBeAtLeast2CharacterException(LastName);
    }
}