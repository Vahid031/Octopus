using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;

public record ChangePasswordCommand : IRequest
{
    public string UserName { get; init; }
    public string Token { get; init; }
    public string Password { get; init; }
}