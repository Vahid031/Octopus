using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithPassword;

internal class SignInWithPasswordCommandHandler : IRequestHandler<SignInWithPasswordCommand, TokenModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SignInWithPasswordCommandHandler> _logger;
    private readonly IUserTokenGenerator _userTokenGenerator;
    private readonly IPasswordDomainService _passwordDomainService;

    public SignInWithPasswordCommandHandler(
        IUserRepository userRepository,
        ILogger<SignInWithPasswordCommandHandler> logger,
        IUserTokenGenerator userTokenGenerator,
        IPasswordDomainService passwordDomainService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _userTokenGenerator = userTokenGenerator;
        _passwordDomainService = passwordDomainService;
    }

    public async Task<TokenModel> Handle(SignInWithPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName);

        if (user == null)
        {
            _logger.LogError("UserName:'{userName}' not found", request.UserName);
            throw new OctopusException("", request.UserName);
        }

        return user.SignInWithPassword(_userTokenGenerator,
            _passwordDomainService, request.Password, request.IpAddress);
    }
}