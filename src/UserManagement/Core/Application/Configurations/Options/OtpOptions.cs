namespace Octopus.UserManagement.Core.Application.Configurations.Options;

public record OtpOptions
{
    public int MaxRetryCount { get; init; }
    public TimeSpan ExpireDuration { get; init; }
}