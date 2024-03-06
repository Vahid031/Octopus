using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserFirstNameMustBeAtLast50CharacterException : OctopusDomainException
{
    public UserFirstNameMustBeAtLast50CharacterException(string firstName) : base("User first name: {0} must be at least 50 character", firstName)
    {
    }
}