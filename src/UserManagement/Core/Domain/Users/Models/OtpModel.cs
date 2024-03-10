namespace Octopus.UserManagement.Core.Domain.Users.Models;

public record OtpModel
{
	public string Code { get; init; }
	public DateTimeOffset Expires { get; init; }
}