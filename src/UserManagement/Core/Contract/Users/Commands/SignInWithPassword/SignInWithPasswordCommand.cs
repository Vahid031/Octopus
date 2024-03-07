using MediatR;
using Octopus.UserManagement.Core.Domain.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;

public record SignInWithPasswordCommand : IRequest<TokenModel>
{
    public string UserName { get; init; }
    public string IpAddress { get; init; }
    public string Password { get; init; }
}