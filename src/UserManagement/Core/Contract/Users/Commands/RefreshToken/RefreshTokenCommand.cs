using MediatR;
using Octopus.UserManagement.Core.Domain.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.RefreshToken;

public record RefreshTokenCommand : IRequest<TokenModel>
{
    public string Token { get; init; }

    public string UserName { get; init; }

    public string IpAddress { get; init; }
}