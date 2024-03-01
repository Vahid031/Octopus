//using MediatR;
//using Octopus.Core.Contract.Exceptions;
//using Octopus.UserManagement.Core.Contract.Users.Commands.ChangePassword;
//using Octopus.UserManagement.Core.Contract.Users.Services;
//using Octopus.UserManagement.Core.Domain.Users.Services;

//namespace Octopus.UserManagement.Core.Application.Users.Commands.ChangePassword;

//internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
//{
//    private readonly IAuthenticationManager _authenticationManager;
//    private readonly IUserRepository _userRepository;

//    public ChangePasswordCommandHandler(IAuthenticationManager authenticationManager,
//        IUserRepository userRepository)
//    {
//        _authenticationManager = authenticationManager;
//        _userRepository = userRepository;
//    }

//    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
//    {

//        var user = _userRepository.GetByPhoneNumber(request.)

//        var user = _userManager.FindById(request.UserId);

//        if (user is null)
//            throw new OctopusException("User not found, userId: {userId}", request.UserId);

//        await _userManager.ChangePassword(request);
//    }
//}