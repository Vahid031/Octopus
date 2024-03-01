namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record ChangePasswordRequest
{
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
    public string ComparePassword { get; init; }
}