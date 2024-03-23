using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Partner.Core.Domain.DistributionCenters.Enums;

namespace Octopus.Partner.Core.Domain.DistributionCenters.Entities;

public class DistributionCenter : AggregateRoot<DistributionCenterId>
{
    #region Properties

    public string Name { get; private protected set; }

    public string ImageUrl { get; private protected set; }

    public DistributionCenterStatusType Status { get; private protected set; }

    private List<BrandInfo> _brands;
    public IReadOnlyCollection<BrandInfo> Brands
    {
        get { return _brands.AsReadOnly(); }
        private protected set { _brands = value.ToList(); }
    }

    public DateTimeOffset CreatedAt { get; private protected set; }

    #endregion

    private DistributionCenter() { }

    public DistributionCenter Create(DistributionCenterId id, string name, string imageUrl)
    {
        return new()
        {
            Name = name,
            ImageUrl = imageUrl,
            CreatedAt = DateTimeOffset.UtcNow,
            Id = id,
            Status = DistributionCenterStatusType.Actived,
            _brands = new List<BrandInfo>()
        };
    }
}
