using Octopus.Core.Domain.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Exceptions;

public class SendOtpCodeException : OctopusDomainException
{
    public SendOtpCodeException()
        : base("Request for new otp code is not permitted, try a few seconds later")
    {
    }
}