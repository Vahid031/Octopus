using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

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
        var user = await _userRepository.GetByPhoneNumber(request.PhoneNumber);

        if (user != null)
        {
            _logger.LogError("UserName:'{username}' already exists", request.UserName);
            throw new OctopusException("UserName:'{username}' already exists", request.UserName);
        }

        user = new User
        {
            FirstName = request.FirstName,
            PhoneNumber = request.PhoneNumber,
            LastName = request.LastName,
            OtpCodes = new(),
            RefreshTokens = new(),
            Id = UserId.New()
        };

        await _userRepository.Insert(user);
        await _userRepository.Commit();
    }
}