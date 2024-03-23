using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class CartId : IdBase<Guid>
{
    private CartId(Guid id)
    {
        Value = id;
    }

    public static CartId Create(Guid id) => new(id);

    public static CartId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CartId, Guid>(value);
    }

    public static CartId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
