using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class CityId : IdBase<Guid>
{
    private CityId(Guid id)
    {
        Value = id;
    }

    public static CityId Create(Guid id) => new(id);

    public static CityId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CityId, Guid>(value);
    }

    public static CityId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}