using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Application.Events;
using Octopus.Core.Contract.Exceptions;
using Octopus.Core.Contract.Services;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SendOtp;

internal class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, DateTimeOffset>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<SendOtpCommandHandler> _logger;
    private readonly IMessageDispatcher _messageDispatcher;
    private readonly IOtpConfiguration _otpConfiguration;

    public SendOtpCommandHandler(IUserRepository userRepository,
        ILogger<SendOtpCommandHandler> logger,
        IMessageDispatcher messageDispatcher,
        IOtpConfiguration otpConfiguration)
    {
        _userRepository = userRepository;
        _logger = logger;
        _messageDispatcher = messageDispatcher;
        _otpConfiguration = otpConfiguration;
    }

    public async Task<DateTimeOffset> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByPhoneNumber(request.PhoneNumber);

        if (user == null)
        {
            _logger.LogError("User with phoneNumber: '{phoneNumber}' not found", request.PhoneNumber);
            throw new OctopusException("User with phoneNumber: '{phoneNumber}' not found", request.PhoneNumber);
        }

        var otp = user.CreateNewOtpCode(_otpConfiguration, request.IpAddress);

        await _userRepository.Update(user);
        await _userRepository.Commit();

        // ToDo: Replace magic word with const
        var message = $"Your code : {otp.Code}";
        var @event = new SendSmsIntegrationEvent("UserManagement", message, user.PhoneNumber.ToString());

        await _messageDispatcher.Raise(@event);

        return otp.Expires;
    }
}