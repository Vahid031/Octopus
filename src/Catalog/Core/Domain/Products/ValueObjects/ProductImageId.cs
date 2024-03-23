using Octopus.Core.Domain.Exceptions;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.ValueObjects;

public class ProductImageId : IdBase<Guid>
{
    private ProductImageId(Guid id)
    {
        Value = id;
    }

    public static ProductImageId Create(Guid id) => new(id);

    public static ProductImageId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<ProductImageId, Guid>(value);
    }

    public static ProductImageId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
