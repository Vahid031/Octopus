using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Contract.Events;

public abstract class IntegrationEvent : INotification
{
    public string Sender { get; private protected set; }
    public UserId AccuredBy { get; private protected set; }
    public Guid EventId { get; private protected set; }

    protected IntegrationEvent(Guid eventId, string sender, UserId accuredBy)
    {
        Sender = sender;
        AccuredBy = accuredBy;
        EventId = eventId;
    }
}
