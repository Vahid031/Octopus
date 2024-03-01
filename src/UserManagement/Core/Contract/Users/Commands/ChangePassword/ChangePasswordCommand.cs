using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;

public record ChangePasswordCommand : IRequest
{
    public string UserId { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
}