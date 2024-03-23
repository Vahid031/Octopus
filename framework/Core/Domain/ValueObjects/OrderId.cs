using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class OrderId : IdBase<Guid>
{
    private OrderId(Guid id)
    {
        Value = id;
    }

    public static OrderId Create(Guid id) => new(id);

    public static OrderId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<OrderId, Guid>(value);
    }

    public static OrderId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}