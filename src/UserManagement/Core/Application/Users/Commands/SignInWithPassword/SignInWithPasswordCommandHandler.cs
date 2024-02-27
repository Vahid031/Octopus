using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithPassword;

internal class SignInWithPasswordCommandHandler : IRequestHandler<SignInWithPasswordCommand>
{
    public Task Handle(SignInWithPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}