using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.ValueObjects;

public class ProductId : IdBase<long>
{
    public ProductId(long id)
    {
        Value = id;
    }
}
