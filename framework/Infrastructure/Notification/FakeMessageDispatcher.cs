using Microsoft.Extensions.Logging;
using Octopus.Core.Contract.Events;
using Octopus.Core.Contract.Services;

namespace Octopus.Infrastructure.Notification;

internal class FakeMessageDispatcher : IMessageDispatcher
{
    private readonly ILogger<FakeMessageDispatcher> _logger;

    public FakeMessageDispatcher(ILogger<FakeMessageDispatcher>logger )
    {
        _logger = logger;
    }
    public Task Raise(IntegrationEvent @event)
    {
        _logger.LogInformation("Message has published, {@event}", @event);
        
        return Task.CompletedTask;
    }
}