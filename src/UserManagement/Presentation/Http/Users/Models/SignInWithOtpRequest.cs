namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInWithOtpRequest
{
    public string Username { get; init; }
    public string Code { get; init; }
}