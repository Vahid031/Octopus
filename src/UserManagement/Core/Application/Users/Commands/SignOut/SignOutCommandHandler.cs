using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignOut;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignOut;

internal class SignOutCommandHandler : IRequestHandler<SignOutCommand>
{
    private readonly IUserManager _userManager;

    public SignOutCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }
    public async Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _userManager.SignOut(request.UserId);
    }
}