
namespace Octopus.Core.Domain.ValueObjects;

public interface IIdBase
{

}

public abstract class IdBase<TKey> : ValueObject<IdBase<TKey>>, IIdBase
{
    public TKey Value { get; protected set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

