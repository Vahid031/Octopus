namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SendOtpRequest
{
    public string PhoneNumber { get; init; }
}