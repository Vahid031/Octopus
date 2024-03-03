using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Mongo.Users;

namespace Octopus.UserManagement.Core.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, MongoUserRepository>();

        services.AddSingleton(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return database.GetCollection<User>(MongoUserRepository.CollectionName);
        });


        BsonSerializer.RegisterSerializer(new UserIdBsonSerializer());

        BsonClassMap.RegisterClassMap<EntityBase<UserId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.MapMember(m => m.FirstName).SetElementName("FirstName");
            cm.MapMember(m => m.LastName).SetElementName("LastName");
            cm.MapMember(m => m.RefreshTokens).SetElementName("RefreshTokens");
            cm.MapMember(m => m.PhoneNumber).SetElementName("PhoneNumber");
            cm.MapMember(m => m.OtpCodes).SetElementName("OtpCodes");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<RefreshToken>(cm =>
        {
            cm.MapMember(m => m.Expires).SetElementName("Expires");
            cm.MapMember(m => m.RevokedByIp).SetElementName("RevokedByIp");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.CreatedByIp).SetElementName("CreatedByIp");
            cm.MapMember(m => m.ReplacedByToken).SetElementName("ReplacedByToken");
            cm.MapMember(m => m.Revoked).SetElementName("Revoked");
            cm.MapMember(m => m.Token).SetElementName("Token");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<OtpCode>(cm =>
        {
            cm.MapMember(m => m.Code).SetElementName("Code");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.CreatedByIp).SetElementName("CreatedByIp");
            cm.MapMember(m => m.RetryCount).SetElementName("RetryCount");
            cm.MapMember(m => m.Revoked).SetElementName("Revoked");
            cm.MapMember(m => m.RevokedByIp).SetElementName("RevokedByIp");
            cm.MapMember(m => m.Expires).SetElementName("Expires");

            cm.SetIgnoreExtraElements(true);
        });

        return services;
    }
}
