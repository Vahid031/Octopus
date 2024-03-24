using MongoDB.Bson.Serialization;
using Octopus.Partner.Core.Domain.DistributionCenters.Entities;

namespace Octopus.UserManagement.Infrastructure.Mongo.Users;

internal class DistributionCenterMapClassExtension
{
    internal static void Register()
    {
        BsonClassMap.RegisterClassMap<DistributionCenter>(cm =>
        {
            cm.MapMember(m => m.Status).SetElementName("Status");
            cm.MapMember(m => m.Brands).SetElementName("Brands");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.ImageUrl).SetElementName("ImageUrl");
            cm.MapMember(m => m.Name).SetElementName("Name");

            cm.SetIgnoreExtraElements(true);
        });
    }
}