using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Events;
using Octopus.Core.Contract.Services;

namespace Octopus.Core.Application.Outboxes;

internal class IntegrationEventPublishWorker(IOutboxRepository outboxRepository,
    IMediator mediator, ILogger<IntegrationEventPublishWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                logger.LogDebug("Process publish events has started");

                var events = await outboxRepository.GetEvents(cancellationToken);

                if (events.Count == 0)
                {
                    logger.LogDebug("New events does not exists");
                    await Task.Delay(2000);
                    continue;
                }

                logger.LogDebug("Events has fetched");

                var tasks = events.Select(e => Publish(e, cancellationToken));

                var taskResults = await Task.WhenAll(tasks);

                logger.LogDebug("Events has published");

                var successEvents = taskResults.Where(m => m.IsSuccess).Select(m => m.EventId).ToList();

                await outboxRepository.MarkAsProceed(successEvents, cancellationToken);

                logger.LogDebug("Success events has marked");

                logger.LogDebug("Process publish events has finished");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Publish events has failed");

                await Task.Delay(5000);
            }
        }
    }

    private async Task<ProcessEventResult> Publish(IntegrationEvent @event, CancellationToken cancellationToken)
    {
        var result = new ProcessEventResult(@event.EventId);
        try
        {
            await mediator.Publish(@event, cancellationToken);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Publish event has failed, event : '{@event}'", @event);
            result.Fail();
        }

        return result;
    }
}
