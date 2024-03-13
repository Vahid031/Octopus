using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Activate;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Deactivate;

internal class DeactivateCommandHandler : IRequestHandler<ActivateCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<DeactivateCommandHandler> _logger;

    public DeactivateCommandHandler(IUserRepository userRepository, ILogger<DeactivateCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(ActivateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found, userId: {userId}", request.UserId);
            throw new OctopusException("User not found, userId: {userId}", request.UserId.ToString());
        }

        user.Deactivate();

        await _userRepository.Update(user);
        await _userRepository.Commit();
    }
}