namespace Octopus.UserManagement.Core.Domain.Users.Models;

public record TokenModel
{
    public string TokenType { get; init; }
    public DateTimeOffset AccessTokenExpires { get; init; }
    public DateTimeOffset RefreshTokenExpires { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public string IpAddress { get; init; }
    public string UserName { get; init; }
    public string PhoneNumber { get; init; }
}
