namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SetPasswordRequest
{
    public string NewPassword { get; init; }
    public string ComparePassword { get; init; }
}