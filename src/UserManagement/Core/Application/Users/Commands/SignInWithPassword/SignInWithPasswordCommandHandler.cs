using MediatR;
using Microsoft.Extensions.Logging;
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
        var user = await _userRepository.GetByUsername(request.Username);

        if (user == null)
        {
            // ToDo: log and throw
            throw new Exception();
        }

        return user.SignInWithPassword(_userTokenGenerator, 
            _passwordDomainService, request.Password, request.IpAddress);
    }
}