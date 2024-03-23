using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class AddressId : IdBase<Guid>
{
    private AddressId(Guid id)
    {
        Value = id;
    }

    public static AddressId Create(Guid id) => new(id);

    public static AddressId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<AddressId, Guid>(value);
    }

    public static AddressId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
