using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithPassword;

internal class SignInWithPasswordCommandHandler : IRequestHandler<SignInWithPasswordCommand>
{
    private readonly IUserManager _userManager;

    public SignInWithPasswordCommandHandler(IUserManager userManager )
    {
        _userManager = userManager;
    }

    public async Task Handle(SignInWithPasswordCommand request, CancellationToken cancellationToken)
    {
        // ToDo: return token
        var result = await _userManager.SignInWithPassword(request);
    }
}