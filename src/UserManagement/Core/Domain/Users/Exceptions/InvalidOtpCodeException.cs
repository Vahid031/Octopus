using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class InvalidOtpCodeException : OctopusDomainException
{
    public InvalidOtpCodeException(string otpCode) : base("Invalid otp code: '{otpCode}'", otpCode)
    {
    }
}