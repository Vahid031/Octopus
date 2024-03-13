using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.SetPassword;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SetPassword;

internal class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand>
{
    private readonly IPasswordDomainService _passwordDomainService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SetPasswordCommandHandler> _logger;

    public SetPasswordCommandHandler(IPasswordDomainService passwordDomainService, IUserRepository userRepository, ILogger<SetPasswordCommandHandler> logger)
    {
        _passwordDomainService = passwordDomainService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(SetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);

        if (user == null)
        {
            _logger.LogInformation("User not found, userId: {userId}", request.UserId);
            throw new OctopusException("User not found, userId: {userId}", request.UserId.ToString());
        }

        user.SetPassword(_passwordDomainService, request.NewPassword);

        await _userRepository.Update(user);
        await _userRepository.Commit();
    }
}