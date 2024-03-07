using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.Core.Application.Events;
using Octopus.Core.Contract.Exceptions;
using Octopus.Core.Contract.Services;
using Octopus.UserManagement.Core.Application.Configurations.Options;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SendOtp;

public record SendOtpCommandHandler : IRequestHandler<SendOtpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SendOtpCommandHandler> _logger;
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly IOptions<OtpOptions> _otpOptions;

    public SendOtpCommandHandler(IUserRepository userRepository,
        ILogger<SendOtpCommandHandler> logger,
        IMessageDispatcher messageDispatcher,
        IOptions<OtpOptions> otpOptions)
    {
        _userRepository = userRepository;
        _logger = logger;
        _messageDispatcher = messageDispatcher;
        _otpOptions = otpOptions;
    }

    public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUserName(request.UserName);

        if (user == null)
        {
            _logger.LogError("UserName:'{userName}' not found", request.UserName);
            throw new OctopusException("UserName:'{userName}' not found", request.UserName);
        }

        var code = user.CreateNewOtpCode(request.IpAddress, _otpOptions.Value.ExpireDuration);

        // ToDo: Replace magic word with const
        var message = $"Your code : {code}";
        var @event = new SendSmsIntegrationEvent("UserManagement", message, user.PhoneNumber.ToString());

        await _messageDispatcher.Raise(@event);
    }
}