using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PasswordMinimumLowerCaseLengthException : OctopusDomainException
{
    public PasswordMinimumLowerCaseLengthException(int minLength) : base("Password Must have at least '{minLength}' lower case characters", minLength.ToString())
    {
    }
}