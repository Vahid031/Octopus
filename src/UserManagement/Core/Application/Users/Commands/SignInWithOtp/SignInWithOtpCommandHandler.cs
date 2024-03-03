using MediatR;
using Octopus.UserManagement.Core.Contract.Users.Commands.SignInWithOtp;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SignInWithOtp;

internal class SignInWithOtpCommandHandler : IRequestHandler<SignInWithOtpCommand, TokenModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserTokenGenerator _tokenGenerator;

    public SignInWithOtpCommandHandler(IUserRepository userRepository, IUserTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<TokenModel> Handle(SignInWithOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsername(request.Username);

        if (user is null)
        {
            //ToDo: log and throw
            throw new Exception();
        }

        return user.SignInWithOtp(_tokenGenerator, request.Code, request.IpAddress);
    }
}