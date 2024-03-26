using MediatR;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.FileManager.Core.Contract.Files.Commands;

public record UploadFileCommand : IRequest<UploadFileCommandResult>
{
    public Stream FileStream { get; init; }

    public string OriginalFileName { get; init; }

    public string FileExtension { get; init; }

    public string FileDirectory { get; init; }

    public long Length { get; init; }

    public string FileType { get; init; }

    public UserId UserId { get; init; }
}


public record UploadFileCommandResult 
{
    public string RelativePath { get; init; }

    public string AbsolutePath { get; init; }
}
