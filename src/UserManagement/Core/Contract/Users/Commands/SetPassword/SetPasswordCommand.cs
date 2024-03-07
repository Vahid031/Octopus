using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SetPassword;

public record SetPasswordCommand : IRequest
{
	public UserId UserId { get; init; }
	public string NewPassword { get; init; }
	public string ComparePassword { get; init; }
}