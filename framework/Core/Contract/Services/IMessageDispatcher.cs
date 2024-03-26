using Octopus.Core.Contract.Events;

namespace Octopus.Core.Contract.Services;

public interface IMessageDispatcher
{
    Task Raise<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : IntegrationEvent;
}
