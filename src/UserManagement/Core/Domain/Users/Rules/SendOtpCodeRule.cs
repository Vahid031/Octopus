using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class SendOtpCodeRule : IBusinessRule
{
    private readonly IEnumerable<OtpCode> _allOtpCodes;

    public SendOtpCodeRule(IEnumerable<OtpCode> allOtpCodes)
    {
        _allOtpCodes = allOtpCodes;
    }

    public void Validate()
    {
        var lastOtpCode = _allOtpCodes.MaxBy(m => m.CreatedAt);

        if (lastOtpCode is null)
            return;

        if (!lastOtpCode.IsActive)
            return;

        throw new SendOtpCodeException();
    }
}