namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SignInWithOtpRequest
{
    public string PhoneNumber { get; init; }
    public string Code { get; init; }
}