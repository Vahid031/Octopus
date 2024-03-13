using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Domain.Entities;

public abstract class EntityBase
{
}

public abstract class EntityBase<TId> : EntityBase where TId : IIdBase
{
    public TId Id { get; protected set; }
}