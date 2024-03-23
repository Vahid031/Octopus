using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class CustomerId : IdBase<Guid>
{
    private CustomerId(Guid id)
    {
        Value = id;
    }

    public static CustomerId Create(Guid id) => new(id);

    public static CustomerId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CustomerId, Guid>(value);
    }

    public static CustomerId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
