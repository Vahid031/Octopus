using MediatR;

namespace Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;

public record SendOtpCommand : IRequest<DateTimeOffset>
{
	public string PhoneNumber { get; init; }
	public string IpAddress { get; init; }
}