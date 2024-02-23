namespace Octopus.Core.Domain.ValueObjects;

public interface IIdBase
{

}

public abstract class IdBase<TKey> : ValueObject<IdBase<TKey>>, IIdBase
{
    public TKey Value { get; protected set; }

    public override int ObjectGetHashCode() => Value.GetHashCode();

    public override bool ObjectIsEqual(IdBase<TKey> other) => Value.Equals(other);
}

