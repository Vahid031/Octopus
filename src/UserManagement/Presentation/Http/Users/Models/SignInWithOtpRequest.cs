namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInWithOtpRequest
{
    public string UserName { get; init; }
    public string Code { get; init; }
}