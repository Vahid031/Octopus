using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class PasswordCompareRule : IBusinessRule
{
    private readonly IPasswordDomainService _passwordDomainService;
    private readonly string _inputPassword;
    private readonly Password _password;

    public PasswordCompareRule(IPasswordDomainService passwordDomainService,
        string inputPassword,
        Password password)
    {
        _passwordDomainService = passwordDomainService;
        _inputPassword = inputPassword;
        _password = password;
    }
    public void Validate()
    {
        if (_password is null)
            throw new UserPasswordMustBeEqualException();

        if (!_passwordDomainService.Equal(_inputPassword, _password.PasswordHash, _password.PasswordSalt))
            throw new UserPasswordMustBeEqualException();
    }
}