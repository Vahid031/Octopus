using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class PhoneNumberMustBeUniqueException : OctopusDomainException
{
    public PhoneNumberMustBeUniqueException(string phoneNumber) : base("PhoneNumber: '{phoneNumber}' already exists", phoneNumber)
    {
    }
}