using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserFirstNameMustBeAtLeast2CharacterException : OctopusDomainException
{
	public UserFirstNameMustBeAtLeast2CharacterException(string firstName) : base("User first name: '{firstName}' must be at least 2 character", firstName)
	{
	}
}