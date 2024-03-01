using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;

public record SignInWithPasswordCommand : IRequest<SignInModel>
{
    public string Username { get; init; }
    public string IpAddress { get; init; }
    public string Password { get; init; }
}