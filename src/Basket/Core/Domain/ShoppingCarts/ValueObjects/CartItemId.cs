using Octopus.Core.Domain.Exceptions;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Basket.Core.Domain.ShoppingCarts.ValueObjects;

public class CartItemId : IdBase<Guid>
{
    private CartItemId(Guid id)
    {
        Value = id;
    }

    public static CartItemId Create(Guid id) => new(id);

    public static CartItemId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<CartItemId, Guid>(value);
    }

    public static CartItemId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
