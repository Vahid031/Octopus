using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserPhoneNumberInvalidException : OctopusDomainException
{
	public UserPhoneNumberInvalidException(string phoneNumber) : base("User phone number: '{phoneNumber}' must be valid", phoneNumber)
	{
	}
}