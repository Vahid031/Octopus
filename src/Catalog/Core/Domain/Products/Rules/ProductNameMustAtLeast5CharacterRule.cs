using Octopus.Catalog.Core.Domain.Products.Exceptions;
using Octopus.Core.Domain.Rules;

namespace Octopus.Catalog.Core.Domain.Products.Rules;

public class ProductNameMustAtLeast5CharacterRule(string Name) : IBusinessRule
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name) || Name.Length < 5)
            throw new ProductNameMustAtLeast5CharacterException(Name);
    }
}
