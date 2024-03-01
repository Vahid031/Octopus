using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithOtp;

internal class SignInWithOtpCommandHandler : IRequestHandler<SignInWithOtpCommand>
{
    private readonly IUserManager _userManager;

    public SignInWithOtpCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        // ToDo: return token
        var result = await _userManager.SignInWithOtp(request);
    }
}