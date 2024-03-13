using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class OtpCodeCheckRule : IBusinessRule
{
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly OtpCode _otpCode;
    private readonly string _code;

    public OtpCodeCheckRule(IOtpConfiguration otpConfiguration, OtpCode otpCode, string code)
    {
        _otpConfiguration = otpConfiguration;
        _otpCode = otpCode;
        _code = code;
    }

    public void Validate()
    {
        if (_otpCode is null || !_otpCode.Code.Equals(_code))
            throw new InvalidOtpCodeException(_code);

        if (!_otpCode.IsActive || _otpConfiguration.MaxRetryCount < _otpCode.RetryCount)
            throw new OtpCodeHasExpiredException(_code);
    }
}