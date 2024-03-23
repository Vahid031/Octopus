using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class CountryId : IdBase<Guid>
{
    private CountryId(Guid id)
    {
        Value = id;
    }

    public static CountryId Create(Guid id) => new(id);

    public static CountryId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CountryId, Guid>(value);
    }

    public static CountryId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
