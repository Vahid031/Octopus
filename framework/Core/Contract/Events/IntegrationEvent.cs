namespace Octopus.Core.Contract.Events;

public abstract class IntegrationEvent
{
    public Guid EventId { get; }
    public string Sender { get; }

    protected IntegrationEvent(Guid eventId, string sender)
    {
        EventId = eventId;
        Sender = sender;
    }
}
