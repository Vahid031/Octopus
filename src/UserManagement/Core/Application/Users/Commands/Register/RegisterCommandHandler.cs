using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(IUserRepository userRepository,
        ILogger<RegisterCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsername(request.Username);

        if (user != null)
        {
            _logger.LogError("UserName:'{username}' already exists", request.Username);
            throw new OctopusException("UserName:'{username}' already exists", request.Username);
        }

        user = User.Create(request.Username, request.PhoneNumber, request.FirstName, request.LastName);


        await _userRepository.Insert(user);
        await _userRepository.Commit();
    }
}