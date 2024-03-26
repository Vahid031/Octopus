namespace Octopus.FileManager.Core.Contract.Files.Services;

public interface IUploadFileService
{
    Task Upload(Stream fileStream, string path, CancellationToken cancellationToken);
}
