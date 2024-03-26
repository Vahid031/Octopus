using Octopus.Core.Contract.Events;

namespace Octopus.Core.Contract.Services;

public interface IOutboxRepository
{
    Task Add<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IntegrationEvent;
    Task<List<IntegrationEvent>> GetEvents(CancellationToken cancellationToken);
    Task MarkAsProceed(List<Guid> events, CancellationToken cancellationToken);
}
