using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Partner.Core.Domain.DistributionCenters.Entities;

public class BrandInfo : EntityBase<BrandId>
{
    #region Properties

    public string Name { get; private protected set; }

    public string ImageUrl { get; private protected set; }

    public int PriorityDesplay { get; private protected set; }

    #endregion

    private BrandInfo() { }

    internal static BrandInfo Create(BrandId id, string name, string imageUrl, int PriorityDesplay = 0)
    {
        return new()
        {
            Id = id,
            ImageUrl = imageUrl,
            PriorityDesplay = PriorityDesplay,
            Name = name,
        };
    }
}
