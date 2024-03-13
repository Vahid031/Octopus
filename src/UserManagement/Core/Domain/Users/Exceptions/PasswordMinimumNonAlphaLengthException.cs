using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PasswordMinimumNonAlphaLengthException : OctopusDomainException
{
    public PasswordMinimumNonAlphaLengthException(int minLength) : base("Password Must have at least '{minLength}' non alpha characters", minLength.ToString())
    {
    }
}