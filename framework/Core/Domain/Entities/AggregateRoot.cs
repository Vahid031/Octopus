using Octopus.Core.Domain.Events;
using Octopus.Core.Domain.Rules;
using Octopus.Core.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Octopus.Core.Domain.Entities;

public abstract class AggregateRoot<TId> : EntityBase<TId> where TId : IIdBase
{
    private readonly List<IDomainEvent> _events;

    protected AggregateRoot() => _events = new List<IDomainEvent>();

    protected AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null) return;
        foreach (var @event in events)
            ((dynamic)this).On((dynamic)@event);
    }

    protected void Apply(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

    public void ClearEvents() => _events.Clear();

    protected static void CheckRule([NotNull] IBusinessRule rule) => rule.Validate();

    protected static Task CheckRuleAsync([NotNull] IAsyncBusinessRule rule) => rule.Validate();
}
