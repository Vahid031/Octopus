using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithOtp;

internal class SignInWithOtpCommandHandler : IRequestHandler<SignInWithOtpCommand, TokenModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserTokenGenerator _tokenGenerator;
    private readonly ILogger<SignInWithOtpCommandHandler> _logger;

    public SignInWithOtpCommandHandler(IUserRepository userRepository, IUserTokenGenerator tokenGenerator, ILogger<SignInWithOtpCommandHandler> logger)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public async Task<TokenModel> Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName);

        if (user is null)
        {
            _logger.LogError("UserName:'{userName}' not found", request.UserName);
            throw new OctopusException("User not found, userName: {userName}", request.UserName.ToString());
        }

        return user.SignInWithOtp(_tokenGenerator, request.Code, request.IpAddress);
    }
}