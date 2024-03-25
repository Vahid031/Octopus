using Microsoft.Extensions.DependencyInjection;

namespace Octopus.FileManager.Infrastructure.Ftp;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerFtpServices(this IServiceCollection services)
    {

        return services;
    }
}
