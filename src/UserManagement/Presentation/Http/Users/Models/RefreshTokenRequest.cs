namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record RefreshTokenRequest
{
    public string UserName { get; init; }

    public string Token { get; init; }
}