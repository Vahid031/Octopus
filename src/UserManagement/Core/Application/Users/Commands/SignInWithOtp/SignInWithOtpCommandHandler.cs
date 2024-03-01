using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Contract.Users.Models;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithOtp;

internal class SignInWithOtpCommandHandler : IRequestHandler<SignInWithOtpCommand, SignInModel>
{
    public Task<SignInModel> Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}