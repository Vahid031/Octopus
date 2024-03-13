using Octopus.UserManagement.Core.Domain.Users.Entities;

namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IOtpConfiguration
{
    bool IsExpired(OtpCode otpCode) => otpCode.CreatedAt.Add(ExpireDuration) <= DateTimeOffset.UtcNow;

    TimeSpan ExpireDuration { get; }

    int MaxRetryCount { get; }
}