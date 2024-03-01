using MediatR;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.ChangePassword;

internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserManager _userManager;

    public ChangePasswordCommandHandler(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.FindById(request.UserId);

        if (user is null)
            throw new OctopusException("User not found, userId: {userId}", request.UserId);

        await _userManager.ChangePassword(request);
    }
}