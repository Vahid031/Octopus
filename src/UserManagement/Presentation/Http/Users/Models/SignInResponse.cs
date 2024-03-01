namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInResponse
{
    public string TokenType { get; init; }
    public long ExpireIn { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}