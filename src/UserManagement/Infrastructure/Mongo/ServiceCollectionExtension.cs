using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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
        UserMapClassExtension.Register();


        return services;
    }
}