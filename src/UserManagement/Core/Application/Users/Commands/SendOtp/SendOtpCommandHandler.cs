using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SendOtp;

public record SendOtpCommandHandler : IRequestHandler<SendOtpCommand>
{
    public Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}