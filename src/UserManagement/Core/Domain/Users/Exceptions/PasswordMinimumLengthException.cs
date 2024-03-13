using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PasswordMinimumLengthException : OctopusDomainException
{
    public PasswordMinimumLengthException(int minLength) : base("Password Must have at least '{minLength}' characters", minLength.ToString())
    {
    }
}