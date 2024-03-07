using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserNameMustBeAtLeast3CharacterException : OctopusDomainException
{
	public UserNameMustBeAtLeast3CharacterException(string name) : base("UserName: '{userName}' must be at least 3 character", name)
	{
	}
}