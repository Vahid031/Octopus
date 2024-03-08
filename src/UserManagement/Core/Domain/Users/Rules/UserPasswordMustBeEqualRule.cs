using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserPasswordMustBeEqualRule(IPasswordDomainService passwordDomainService, string inputPassword, Password password) : IBussinessRule
{
    public void Validate()
    {
        if (password is null)
            throw new UserPasswordMustBeEqualException();

        if (!passwordDomainService.Equal(inputPassword, password.PasswordHash, password.PasswordSalt))
            throw new UserPasswordMustBeEqualException();
    }
}
