using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class ProductId : IdBase<Guid>
{
    private ProductId(Guid id)
    {
        Value = id;
    }

    public static ProductId Create(Guid id) => new(id);

    public static ProductId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<ProductId, Guid>(value);
    }

    public static ProductId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
