using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.ChangePassword;

internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IPasswordDomainService _passwordDomainService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(IPasswordDomainService passwordDomainService, IUserRepository userRepository, ILogger<ChangePasswordCommandHandler> logger)
    {
        _passwordDomainService = passwordDomainService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            _logger.LogInformation("User not found, userId: {userId}", request.UserId);
            throw new OctopusException("User not found, userId: {userId}", request.UserId.ToString());
        }

        user.ChangePassword(_passwordDomainService, request.OldPassword, request.NewPassword);

        await _userRepository.Update(user);
        await _userRepository.Commit();
    }
}