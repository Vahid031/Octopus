using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserLastNameMustBeAtLast50CharacterException : OctopusDomainException
{
	public UserLastNameMustBeAtLast50CharacterException(string firstName) : base("User last name: '{lastName}' must be at least 50 character", firstName)
	{
	}
}