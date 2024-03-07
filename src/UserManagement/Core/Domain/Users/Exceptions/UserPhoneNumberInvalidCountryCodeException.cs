using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class UserPhoneNumberInvalidCountryCodeException : OctopusDomainException
{
	public UserPhoneNumberInvalidCountryCodeException(string countryCode) : base("User phone number: '{phoneNumber}' country code is invalid", countryCode)
	{
	}
}