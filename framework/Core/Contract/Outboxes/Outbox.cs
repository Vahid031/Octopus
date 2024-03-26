using Octopus.Core.Contract.Events;

namespace Octopus.Core.Contract.Outboxes;

public class Outbox
{
    public Guid Id { get; private protected set; }
    public DateTimeOffset AccuredOn { get; private protected set; }
    public int TryCount { get; private protected set; }
    public bool IsProcessed { get; private protected set; }
    public IntegrationEvent Event { get; private protected set; }

    private Outbox(IntegrationEvent @event)
    {
        Id = @event.EventId;
        AccuredOn = DateTimeOffset.UtcNow;
        IsProcessed = false;
        Event = @event;
        TryCount = 0;
    }

    public static Outbox Create(IntegrationEvent @event) => new(@event);
}