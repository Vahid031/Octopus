using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.Activate;

public class ActivateCommand : IRequest
{
	public UserId UserId { get; init; }
}