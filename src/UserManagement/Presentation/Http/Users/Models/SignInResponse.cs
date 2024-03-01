namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInResponse
{
    public string UserName { get; init; }
    public List<string> Roles { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}