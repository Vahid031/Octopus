using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class SignInWithOtpCodeRule : IBusinessRule
{
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly OtpCode _otpCode;
    private readonly string _code;

    public SignInWithOtpCodeRule(IOtpConfiguration otpConfiguration, OtpCode otpCode, string code)
    {
        _otpConfiguration = otpConfiguration;
        _otpCode = otpCode;
        _code = code;
    }

    public void Validate()
    {
        if (_otpCode is null || !_otpCode.Code.Equals(_code))
            throw new InvalidOtpCodeException(_code);

        if (_otpCode.IsRevoked || _otpConfiguration.MaxRetryCount < _otpCode.RetryCount || _otpConfiguration.IsExpired(_otpCode))
            throw new OtpCodeHasExpiredException(_code);
    }
}