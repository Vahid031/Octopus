using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Core.Domain.Entities;

public abstract class EntityBase<TId> where TId : IIdBase
{
    public TId Id { get; set; }
}