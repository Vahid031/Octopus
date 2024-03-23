using Octopus.Core.Domain.Services;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Partner.Core.Domain.DistributionCenters.Entities;

namespace Octopus.Partner.Core.Domain.DistributionCenters.Services;

public interface IDistributionCenterRepository : IRepository<DistributionCenter, DistributionCenterId>
{
}
