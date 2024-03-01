using Octopus.Core.Contract.Events;

namespace Octopus.Core.Contract.Services;

public interface IMessageDispacher
{
    Task Raise(IntegrationEvent @event);
}
