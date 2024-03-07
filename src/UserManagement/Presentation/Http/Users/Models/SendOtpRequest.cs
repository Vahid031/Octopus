namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SendOtpRequest
{
    public string UserName { get; init; }
}