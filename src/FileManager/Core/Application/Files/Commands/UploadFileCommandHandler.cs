using MediatR;
using Microsoft.Extensions.Options;
using Octopus.FileManager.Core.Application.Configuration;
using Octopus.FileManager.Core.Contract.Files.Commands;
using Octopus.FileManager.Core.Contract.Files.Services;
using Octopus.FileManager.Core.Domain.Files.Services;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;
using File = Octopus.FileManager.Core.Domain.Files.Entities.File;

namespace Octopus.FileManager.Core.Application.Files.Commands;

internal class UploadFileCommandHandler(
    IUploadFileService uploadFileService,
    IOptions<CdnOptions> options,
    IFileRepository fileRepository)
    : IRequestHandler<UploadFileCommand, UploadFileCommandResult>
{
    public async Task<UploadFileCommandResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var id = FileId.New();

        var path = GetRelativePath(request.FileDirectory, id.ToString(), request.FileExtension);

        await uploadFileService.Upload(request.FileStream, path, cancellationToken);

        var file = File.Create(id, path, request.OriginalFileName, request.Length,
            request.FileType, request.UserId);

        await fileRepository.Insert(file);
        await fileRepository.Commit();

        return new UploadFileCommandResult
        {
            RelativePath = file.RelativePath,
            AbsolutePath = GetAbsolutePath(file.RelativePath)
        };
    }

    private string GetAbsolutePath(string relativePath)
        => Path.Combine(options.Value.Address, relativePath).Replace("\\", "/");

    private string GetRelativePath(string directory, string name, string extension)
        => Path.Combine(directory.ToLower(), $"{name}{extension}").Replace("\\", "/");
}
