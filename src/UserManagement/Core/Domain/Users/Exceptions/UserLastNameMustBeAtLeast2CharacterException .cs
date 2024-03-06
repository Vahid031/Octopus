using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserLastNameMustBeAtLeast2CharacterException : OctopusDomainException
{
    public UserLastNameMustBeAtLeast2CharacterException(string firstName) : base("User last name: {0} must be at least 2 character", firstName)
    {
    }
}