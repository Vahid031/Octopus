using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserNameMustBeAtLast150CharacterException : OctopusDomainException
{
	public UserNameMustBeAtLast150CharacterException(string name) : base("UserName: '{userName}' must be at last 150 character", name)
	{
	}
}