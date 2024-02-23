using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.ValueObjects;

public class ProductId : IdBase<Guid>
{
    private ProductId(Guid id)
    {
        Value = id;
    }

    public static ProductId Create(Guid id) => new(id);

    public static ProductId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
