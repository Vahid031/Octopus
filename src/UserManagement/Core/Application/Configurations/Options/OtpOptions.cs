namespace Octopus.UserManagement.Core.Application.Configurations.Options;

public record OtpOptions
{
    public int MaxRetryCount { get; init; } = 3;
    public TimeSpan ExpireDuration { get; init; } = TimeSpan.FromMinutes(15);
}