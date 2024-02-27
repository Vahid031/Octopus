using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;

public record SendOtpCommand : IRequest
{
    public string UserName { get; init; }
}