using Octopus.Core.Contract.Events;

namespace Octopus.Core.Contract.Services;

public interface IMessageDispatcher
{
    Task Raise(IntegrationEvent @event);
}
