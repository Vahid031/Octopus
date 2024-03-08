using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class OtpCodeMustBeValidRule(List<OtpCode> otpCodes, string code) : IBussinessRule
{
    public void Validate()
    {
        var otpCode = otpCodes?.LastOrDefault();

        if (otpCode == null ||
            !otpCode.Code.Equals(code) ||
            otpCode.IsExpired)
            throw new UserOtpCodeInvalidException(code);
    }
}