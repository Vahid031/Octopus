using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithOtp;

internal class SignInWithOtpCommandHandler : IRequestHandler<SignInWithOtpCommand>
{
    public Task Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}