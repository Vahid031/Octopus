using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Models;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithPassword;

internal class SignInWithPasswordCommandHandler : IRequestHandler<SignInWithPasswordCommand, SignInModel>
{
    private readonly IUserManager _userManager;

    public SignInWithPasswordCommandHandler(IUserManager userManager )
    {
        _userManager = userManager;
    }

    public async Task<SignInModel> Handle(SignInWithPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _userManager.SignInWithPassword(request);
    }
}