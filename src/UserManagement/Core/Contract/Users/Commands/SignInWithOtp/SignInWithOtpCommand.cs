using MediatR;
using Octopus.UserManagement.Core.Domain.Users.Models;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;

public record SignInWithOtpCommand : IRequest<TokenModel>
{
    public string PhoneNumber { get; init; }
    public string IpAddress { get; init; }
    public string Code { get; init; }
}