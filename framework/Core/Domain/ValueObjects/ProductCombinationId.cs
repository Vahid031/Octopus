using Octopus.Core.Domain.Exceptions;

namespace Octopus.Core.Domain.ValueObjects;

public class ProductCombinationId : IdBase<Guid>
{
    private ProductCombinationId(Guid id)
    {
        Value = id;
    }

    public static ProductCombinationId Create(Guid id) => new(id);

    public static ProductCombinationId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<ProductCombinationId, Guid>(value);
    }

    public static ProductCombinationId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}