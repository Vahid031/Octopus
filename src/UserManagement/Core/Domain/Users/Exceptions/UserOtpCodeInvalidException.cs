using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserOtpCodeInvalidException : OctopusDomainException
{
	public UserOtpCodeInvalidException(string otpCode) : base("User otp code: '{otpCode}' is invalid", otpCode)
	{
	}
}