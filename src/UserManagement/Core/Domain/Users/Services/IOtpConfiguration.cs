namespace Octopus.UserManagement.Core.Domain.Users.Services;

public interface IOtpConfiguration
{
    TimeSpan ExpireDuration { get; }
    int MaxRetryCount { get; }
}