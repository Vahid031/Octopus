using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserPasswordMustBeEqualException : OctopusDomainException
{
    public UserPasswordMustBeEqualException() : base("User password: user password is incorrect")
    {
    }
}