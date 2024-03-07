namespace Octopus.UserManagement.Presentation.Http.Users.Models;

public record SetPasswordWithOtpRequest
{
	public string NewPassword { get; init; }
	public string ComparePassword { get; init; }
}