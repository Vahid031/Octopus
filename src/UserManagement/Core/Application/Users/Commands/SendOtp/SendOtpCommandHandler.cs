using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Application.Events;
using Octopus.Core.Contract.Exceptions;
using Octopus.Core.Contract.Services;
using Octopus.UserManagement.Core.Contract.Users.Commands.SendOtp;
using Octopus.UserManagement.Core.Contract.Users.Services;

namespace Octopus.UserManagement.Core.Application.Users.Commands.SendOtp;

public record SendOtpCommandHandler : IRequestHandler<SendOtpCommand>
{
    private readonly IUserManager _userManager;
    private readonly ILogger<SendOtpCommandHandler> _logger;
    private readonly IMessageDispacher _messageDispacher;

    public SendOtpCommandHandler(IUserManager userManager, ILogger<SendOtpCommandHandler> logger, IMessageDispacher messageDispacher)
    {
        _userManager = userManager;
        _logger = logger;
        _messageDispacher = messageDispacher;
    }

    public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByUserName(request.UserName);

        if (user == null)
        {
            _logger.LogError("UserName:'{username}' not found", request.UserName);
            throw new OctopusException("UserName:'{username}' not found", request.UserName);
        }

        var code = await _userManager.CreateOtpCode(user.UserName);

        var message = string.Format(@"Your code : {code}", code);

        var @event = new SendSmsIntegrationEvent("UserManagement", message, user.PhoneNumber);

        await _messageDispacher.Raise(@event);
    }
}