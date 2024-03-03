using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;

public record ChangePasswordCommand : IRequest
{
    public UserId UserId { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}