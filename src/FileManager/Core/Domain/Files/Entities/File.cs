using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;

namespace Octopus.FileManager.Core.Domain.Files.Entities;

public class File : AggregateRoot<FileId>
{
    #region Properties

    public string Type { get; private protected set; }

    public string RelativePath { get; private protected set; }

    public long Length { get; private protected set; }

    public string OriginalName { get; private protected set; }

    public DateTimeOffset CreatedAt { get; private protected set; }

    public UserId CreatedBy { get; private protected set; }

    #endregion

    private File() { }

    private File(FileId id, string relativePath, string originalName, long length,
        string type, UserId createdBy)
    {
        RelativePath = relativePath;
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
        Length = length;
        OriginalName = originalName;
        Id = id;
        Type = type;
    }

    public static File Create(FileId id, string relativePath, string originalName, long length,
        string type, UserId createdBy)
    {
        return new(id, relativePath, originalName, length, type, createdBy);
    }
}
