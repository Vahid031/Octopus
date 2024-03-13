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
    private readonly IOtpConfiguration _otpConfiguration;
    private readonly ILogger<SignInWithOtpCommandHandler> _logger;

    public SignInWithOtpCommandHandler(IUserRepository userRepository,
        IUserTokenGenerator tokenGenerator,
        IOtpConfiguration otpConfiguration,
        ILogger<SignInWithOtpCommandHandler> logger)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _otpConfiguration = otpConfiguration;
        _logger = logger;
    }

    public async Task<TokenModel> Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByPhoneNumber(request.PhoneNumber);

        if (user is null)
        {
            _logger.LogError("User with phone number: '{phoneNumber}' not found", request.PhoneNumber);
            throw new OctopusException("User with phone number: '{phoneNumber}' not found", request.PhoneNumber);
        }

        var result = user.SignInWithOtp(_tokenGenerator, _otpConfiguration, request.Code, request.IpAddress);

        await _userRepository.Update(user);
        await _userRepository.Commit();

        return result;
    }
}