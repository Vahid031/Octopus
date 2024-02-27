using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    public Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}