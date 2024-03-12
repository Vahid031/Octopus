namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SendOtpResponse
{
	public DateTimeOffset Expires { get; init; }
}