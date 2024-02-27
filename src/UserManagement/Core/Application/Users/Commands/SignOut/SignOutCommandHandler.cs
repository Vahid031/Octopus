using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignOut;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignOut;

internal class SignOutCommandHandler : IRequestHandler<SignOutCommand>
{
    public Task Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}