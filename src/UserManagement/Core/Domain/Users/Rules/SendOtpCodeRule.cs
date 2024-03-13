using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class SendOtpCodeRule : IBusinessRule
{
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly IEnumerable<OtpCode> _allOtpCodes;

    public SendOtpCodeRule(IOtpConfiguration otpConfiguration, IEnumerable<OtpCode> allOtpCodes)
    {
        _otpConfiguration = otpConfiguration;
        _allOtpCodes = allOtpCodes;
    }

    public void Validate()
    {
        var lastOtpCode = _allOtpCodes.MaxBy(m => m.CreatedAt);

        if (lastOtpCode is null || lastOtpCode.IsRevoked)
            return;

        if (_otpConfiguration.IsExpired(lastOtpCode))
            return;

        throw new SendOtpCodeException();
    }
}