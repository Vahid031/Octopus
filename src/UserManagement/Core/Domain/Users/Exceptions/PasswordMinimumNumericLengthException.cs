using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PasswordMinimumNumericLengthException : OctopusDomainException
{
    public PasswordMinimumNumericLengthException(int minLength) : base("Password Must have at least '{minLength}' numeric characters", minLength.ToString())
    {
    }
}