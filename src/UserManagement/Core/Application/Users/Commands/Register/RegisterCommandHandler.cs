using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserManager _userManager;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUserManager userManager, ILogger<RegisterCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByUserName(request.UserName);

        if (user != null)
        {
            _logger.LogError("UserName:'{username}' already exists", request.UserName);
            throw new OctopusException("UserName:'{username}' already exists", request.UserName);
        }

        await _userManager.Create(request);
    }
}