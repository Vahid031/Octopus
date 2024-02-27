using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignOut;

public record SignOutCommand : IRequest
{
    public string UserId { get; init; }
}