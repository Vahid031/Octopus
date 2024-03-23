using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class ProvinceId : IdBase<Guid>
{
    private ProvinceId(Guid id)
    {
        Value = id;
    }

    public static ProvinceId Create(Guid id) => new(id);

    public static ProvinceId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<ProvinceId, Guid>(value);
    }

    public static ProvinceId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
