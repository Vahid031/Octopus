using Octopus.Core.Domain.Exceptions;

namespace Octopus.Catalog.Core.Domain.Products.Exceptions;

public class ProductNameMustAtLeast5CharacterException : DomainException
{
    public ProductNameMustAtLeast5CharacterException(string name)
        : base("Product name: '{0}' must at least 5 character", name)
    {
    }
}
