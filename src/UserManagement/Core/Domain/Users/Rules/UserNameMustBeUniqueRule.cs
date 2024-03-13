using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class UserNameMustBeUniqueRule(IUserRepository UserRepository, string UserName) : IBusinessRule
{
	public void Validate()
	{
		if (UserRepository.Exists(UserName))
			throw new UserNameMustBeUniqueException(UserName);
	}
}