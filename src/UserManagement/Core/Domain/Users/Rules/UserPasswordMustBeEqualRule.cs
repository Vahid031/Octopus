using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserPasswordMustBeEqualRule(IPasswordDomainService passwordDomainService, string Password, string PasswordHash, string PasswordSalt) : IBussinessRule
{
	public void Validate()
	{
		if (!passwordDomainService.Equal(Password, PasswordHash, PasswordSalt))
			throw new UserPasswordMustBeEqualException();
	}
}