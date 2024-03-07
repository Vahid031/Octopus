using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SetPasswordWithOtp;

public record SetPasswordWithOtpCommand : IRequest
{
    public UserId UserId { get; set; }
    public string Code { get; init; }
    public string NewPassword { get; init; }
    public string ComparePassword { get; init; }
}