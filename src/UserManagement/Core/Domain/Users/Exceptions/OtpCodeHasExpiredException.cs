using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class OtpCodeHasExpiredException : OctopusDomainException
{
    public OtpCodeHasExpiredException(string otpCode) : base("Otp code: '{otpCode}' has expired", otpCode)
    {
    }
}