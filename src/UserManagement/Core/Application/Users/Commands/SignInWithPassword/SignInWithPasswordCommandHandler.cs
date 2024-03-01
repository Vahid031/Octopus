using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithPassword;
using Octopus.UserManagement.Core.Contract.Users.Models;
using Octopus.UserManagement.Core.Contract.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithPassword;

internal class SignInWithPasswordCommandHandler : IRequestHandler<SignInWithPasswordCommand, SignInModel>
{
    private readonly IAuthenticationManager _authenticationManager;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SignInWithPasswordCommandHandler> _logger;

    public SignInWithPasswordCommandHandler(IAuthenticationManager authenticationManager,
        IUserRepository userRepository,
        ILogger<SignInWithPasswordCommandHandler> logger)
    {
        _authenticationManager = authenticationManager;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<SignInModel> Handle(SignInWithPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByPhoneNumber(request.Username);

        if (user == null)
        {
            user = new User
            {
                FirstName = "Vahid",
                PhoneNumber = "09212681463",
                LastName = "Goodarzi",
                OtpCodes = new(),
                RefreshTokens = new(),
                Id = UserId.New()
            };

            await _userRepository.Insert(user);
            await _userRepository.Commit();
        }




        var signInUserModel = new SignInUserModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserId = user.Id.ToString(),
            Roles = new string[] { "", "" }.ToList(),
        };

        var result = await _authenticationManager.SignIn(signInUserModel);

        //ToDo: persist result

        return result;
    }
}