using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class LotId : IdBase<Guid>
{
    private LotId(Guid id)
    {
        Value = id;
    }

    public static LotId Create(Guid id) => new(id);

    public static LotId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<LotId, Guid>(value);
    }

    public static LotId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}