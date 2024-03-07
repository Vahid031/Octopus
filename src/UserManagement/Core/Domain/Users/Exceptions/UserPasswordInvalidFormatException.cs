using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserPasswordInvalidFormatException : OctopusDomainException
{
	public UserPasswordInvalidFormatException() : base("User password: invalid format")
	{
	}
}