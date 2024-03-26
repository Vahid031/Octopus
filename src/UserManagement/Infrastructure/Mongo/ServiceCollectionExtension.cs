using Microsoft.Extensions.DependencyInjection;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Infrastructure.Mongo.Users;

namespace Octopus.UserManagement.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUserManagementMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, MongoUserRepository>();

        services.AddMongoCollection<User, UserId>(MongoUserRepository.CollectionName);

        UserMapClassExtension.Register();


        return services;
    }
}