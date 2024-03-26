using Microsoft.Extensions.Options;
using Octopus.FileManager.Core.Contract.Files.Services;
using Octopus.FileManager.Infrastructure.Ftp.Configuration;
using FluentFTP;

namespace Octopus.FileManager.Infrastructure.Ftp.Files;

internal class FtpUploadFileService : IUploadFileService
{
    private readonly IOptions<FtpOptions> _options;

    public FtpUploadFileService(IOptions<FtpOptions> options)
    {
        _options = options;
    }

    public async Task Upload(Stream fileStream, string path, CancellationToken cancellationToken)
    {
        using var client = new AsyncFtpClient(_options.Value.Address, _options.Value.UserName,
            _options.Value.Password, _options.Value.Port);

        var response = await client.UploadStream(fileStream, path, FtpRemoteExists.Overwrite, true,
            token: cancellationToken);

        if (response == FtpStatus.Success)
            return;

        //ToDo: log and throw exception
        throw new Exception("file upload");
    }
}
