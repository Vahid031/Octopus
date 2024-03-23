using MongoDB.Driver;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo.Shared;
using Octopus.Partner.Core.Domain.DistributionCenters.Entities;
using Octopus.Partner.Core.Domain.DistributionCenters.Services;

namespace Octopus.UserManagement.Core.Mongo.Users;

internal class MongoDistributionCenterRepository 
    : MongoRepositoryBase<DistributionCenter, DistributionCenterId>, IDistributionCenterRepository
{
    public const string CollectionName = "DistributionCenters";
    public MongoDistributionCenterRepository(IUnitOfWork unitOfWork, IMongoCollection<DistributionCenter> collection)
        : base(unitOfWork, collection)
    {
    }
}
