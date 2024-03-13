using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserNameMustBeUniqueException : OctopusDomainException
{
	public UserNameMustBeUniqueException(string userName) : base("UserName: '{userName}' already exists", userName)
	{
	}
}