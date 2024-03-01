namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SendOtpRequest
{
    public string Username { get; init; }
}