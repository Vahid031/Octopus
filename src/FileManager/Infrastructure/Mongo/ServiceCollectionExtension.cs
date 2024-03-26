using Microsoft.Extensions.DependencyInjection;
using Octopus.FileManagement.Infrastructure.Mongo.Files;
using Octopus.FileManager.Core.Domain.Files.Services;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;
using Octopus.Infrastructure.Mongo;
using Octopus.UserManagement.Infrastructure.Mongo.Users;
using File = Octopus.FileManager.Core.Domain.Files.Entities.File;


namespace Octopus.FileManager.Infrastructure.Mongo;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerMongoServices(this IServiceCollection services)
    {
        services.AddScoped<IFileRepository, MongoFileRepository>();

        services.AddMongoCollection<File, FileId>(MongoFileRepository.CollectionName);

        FileMapClassExtension.Register();

        return services;
    }
}
