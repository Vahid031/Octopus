namespace Octopus.UserManagement.Presentation.Http.Configurations.Options;

public record JwtOptions
{
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public TimeSpan TokenDuration { get; init; }
    public TimeSpan RefreshTokenDuration { get; init; }
}