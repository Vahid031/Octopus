using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;

public record SignInWithOtpCommand : IRequest
{
    public string UserName { get; init; }
    public string IpAddress { get; init; }
    public string Code { get; init; }
}