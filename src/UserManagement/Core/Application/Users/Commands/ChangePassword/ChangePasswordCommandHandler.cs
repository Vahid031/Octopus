using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;

namespace Octopus.UserManagement.Core.Application.Users.Commands.ChangePassword;

internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    public Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}