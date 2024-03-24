using Octopus.Catalog.Core.Domain.Products.Enums;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Catalog.Core.Domain.Products.Entities;

public class Product : AggregateRoot<ProductId>
{
    #region Properties

    public string Name { get; private protected set; }

    public string Description { get; private protected set; }

    public string Upc { get; private protected set; }

    private List<ProductImageInfo> _images;
    public IReadOnlyCollection<ProductImageInfo> Images
    {
        get { return _images.AsReadOnly(); }
        private protected set { _images = value.ToList(); }
    }

    public ProductStatusType Status { get; private protected set; }

    public BrandInfo Brand { get; private protected set; }

    private List<CategoryInfo> _categories;
    public IReadOnlyCollection<CategoryInfo> Categories
    {
        get { return _categories.AsReadOnly(); }
        private protected set { _categories = value.ToList(); }
    }

    public decimal Weight { get; private protected set; }

    public decimal Length { get; private protected set; }

    public decimal Width { get; private protected set; }

    public decimal Height { get; private protected set; }

    #endregion

}
