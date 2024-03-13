using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.RefreshToken;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.RefreshToken;

internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenModel>
{
    private readonly IUserTokenGenerator _userTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RefreshTokenCommandHandler(IUserTokenGenerator userTokenGenerator,
        IUserRepository userRepository,
        ILogger<RefreshTokenCommandHandler> logger)
    {
        _userTokenGenerator = userTokenGenerator;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<TokenModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName);

        if (user == null)
        {
            _logger.LogError("User with username: '{userName}' not found", request.UserName);
            throw new OctopusException("User with username: '{userName}' not found", request.UserName);
        }

        var result = user.RefreshToken(_userTokenGenerator, request.Token, request.IpAddress);

        await _userRepository.Update(user);
        await _userRepository.Commit();

        return result;
    }
}