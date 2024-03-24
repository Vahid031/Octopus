using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class InventoryId : IdBase<Guid>
{
    private InventoryId(Guid id)
    {
        Value = id;
    }

    public static InventoryId Create(Guid id) => new(id);

    public static InventoryId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<InventoryId, Guid>(value);
    }

    public static InventoryId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
