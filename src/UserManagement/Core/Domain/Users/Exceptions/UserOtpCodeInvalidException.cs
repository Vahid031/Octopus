using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserOtpCodeInvalidException : OctopusDomainException
{
    public UserOtpCodeInvalidException(string otpCode) : base("User otp code: {0} is invalid", otpCode)
    {
    }
}