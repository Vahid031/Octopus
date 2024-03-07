using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Exceptions;
using Octopus.UserManagement.Core.Contract.Users.Commands.Register;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.Register;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
	private readonly IUserRepository _userRepository;
	private readonly ILogger<RegisterCommandHandler> _logger;

	public RegisterCommandHandler(IUserRepository userRepository,
		ILogger<RegisterCommandHandler> logger)
	{
		_userRepository = userRepository;
		_logger = logger;
	}

	public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByUserName(request.UserName);

		if (user != null)
		{
			_logger.LogError("UserName:'{userName}' already exists", request.UserName);
			throw new OctopusException("UserName:'{userName}' already exists", request.UserName);
		}

		user = User.Create(_userRepository, request.UserName, request.PhoneNumber, request.FirstName, request.LastName);


		await _userRepository.Insert(user);
		await _userRepository.Commit();
	}
}