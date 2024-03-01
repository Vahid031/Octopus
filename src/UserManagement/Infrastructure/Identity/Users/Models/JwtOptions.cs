namespace Octopus.UserManagement.Core.Identity.Users.Models;

public class JwtOptions
{
    public string Key { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public TimeSpan TokenDuration { get; init; }
    public TimeSpan RefereshTokenDuration { get; init; }
}
