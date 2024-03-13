using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PasswordMinimumUpperCaseLengthException : OctopusDomainException
{
    public PasswordMinimumUpperCaseLengthException(int minLength) : base("Password Must have at least '{minLength}' upper case characters", minLength.ToString())
    {
    }
}