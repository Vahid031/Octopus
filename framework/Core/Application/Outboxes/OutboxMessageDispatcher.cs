using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Events;
using Octopus.Core.Contract.Services;

namespace Octopus.Core.Application.Outboxes;

internal class OutboxMessageDispatcher(ILogger<OutboxMessageDispatcher> logger,
    IOutboxRepository outBoxRepository) : IMessageDispatcher
{
    public async Task Raise<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IntegrationEvent
    {
        logger.LogInformation("Message has published, {@event}", @event);

        await outBoxRepository.Add(@event, cancellationToken);
    }
}