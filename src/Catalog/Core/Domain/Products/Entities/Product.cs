using Octopus.Catalog.Core.Domain.Products.Rules;
using Octopus.Catalog.Core.Domain.Products.ValueObjects;
using Octopus.Core.Domain.Entities;

namespace Octopus.Catalog.Core.Domain.Products.Entities;

public class Product : AggregateRoot<ProductId>
{
    private Product() { }

    private Product(string name, string code, string sku)
    {
        CheckRule(new ProductNameMustAtLeast5CharacterRule(name));

        Name = name;
        Code = code;
        Sku = sku;
    }

    public static Product Create(string name, string code, string sku)
        => new(name, code, sku);

    public string Name { get; private protected set; }
    public string Code { get; private protected set; }
    public string Sku { get; private protected set; }
    public long Price { get; private protected set; }

    public void SetPrice(long price)
    {

    }
}
