using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.SetPasswordWithOtp;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SetPasswordWithOtp;

public class SetPasswordWithOtpCommandHandler : IRequestHandler<SetPasswordWithOtpCommand>
{
    private readonly IPasswordDomainService _passwordDomainService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SetPasswordWithOtpCommandHandler> _logger;

    public SetPasswordWithOtpCommandHandler(IPasswordDomainService passwordDomainService, IUserRepository userRepository, ILogger<SetPasswordWithOtpCommandHandler> logger)
    {
        _passwordDomainService = passwordDomainService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Handle(SetPasswordWithOtpCommand request, CancellationToken cancellationToken)
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