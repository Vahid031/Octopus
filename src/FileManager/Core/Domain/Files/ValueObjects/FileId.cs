using Octopus.Core.Domain.Exceptions;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.FileManager.Core.Domain.Files.ValueObjects;

public class FileId : IdBase<Guid>
{
    private FileId(Guid id)
    {
        Value = id;
    }

    public static FileId Create(Guid id) => new(id);

    public static FileId Create(string value)
    {
        if (Guid.TryParse(value, out Guid id))
            return new(id);

        throw new InvalidArgumentToCreateValueObjectException<FileId, Guid>(value);
    }

    public static FileId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("N");
}
