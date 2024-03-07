namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInWithPasswordRequest
{
    public string UserName { get; init; }
    public string Password { get; init; }
}