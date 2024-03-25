namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SendOtpResponse
{
	public TimeSpan Expires { get; init; }
}