using MediatR;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Events;

namespace Octopus.Core.Application.Events;

internal class SendSmsIntegrationEventHandler(ILogger<SendSmsIntegrationEventHandler> logger) : INotificationHandler<SendSmsIntegrationEvent>
{
    public Task Handle(SendSmsIntegrationEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Message has published, {notification}", notification);

        return Task.CompletedTask;
    }
}
