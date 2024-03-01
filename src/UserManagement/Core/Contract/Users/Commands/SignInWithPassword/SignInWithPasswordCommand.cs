using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;

public record SignInWithPasswordCommand : IRequest
{
    public string UserName { get; init; }
    public string IpAddress { get; init; }
    public string Password { get; init; }
}