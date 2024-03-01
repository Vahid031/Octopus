using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;

public record SignInWithOtpCommand : IRequest<SignInModel>
{
    public string Username { get; init; }
    public string IpAddress { get; init; }
    public string Code { get; init; }
}