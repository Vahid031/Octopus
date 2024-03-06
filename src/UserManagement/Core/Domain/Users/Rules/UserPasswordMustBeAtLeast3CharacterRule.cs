using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserPasswordMustBeAtLeast3CharacterRule(string Password) : IBussinessRule
{
    public void Validate()
    {
        if (Password.Length < 3)
            throw new UserPasswordMustBeAtLeast3CharacterException(Password);
    }
}