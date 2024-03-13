using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Activate;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Activate;

internal class ActivateCommandHandler : IRequestHandler<ActivateCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ActivateCommandHandler> _logger;

    public ActivateCommandHandler(IUserRepository userRepository, ILogger<ActivateCommandHandler> logger)
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

        user.Active();

        await _userRepository.Update(user);
        await _userRepository.Commit();
    }
}