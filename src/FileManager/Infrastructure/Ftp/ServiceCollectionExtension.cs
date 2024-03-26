using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Octopus.FileManager.Core.Contract.Files.Services;
using Octopus.FileManager.Infrastructure.Ftp.Configuration;
using Octopus.FileManager.Infrastructure.Ftp.Files;

namespace Octopus.FileManager.Infrastructure.Ftp;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFileManagerFtpServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOptions<FtpOptions>()
         .Bind(configuration.GetSection(nameof(FtpOptions)));

        services.AddSingleton<IUploadFileService, FtpUploadFileService>();

        return services;
    }
}
