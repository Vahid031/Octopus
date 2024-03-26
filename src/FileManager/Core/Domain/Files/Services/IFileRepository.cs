using Octopus.Core.Domain.Services;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;

namespace Octopus.FileManager.Core.Domain.Files.Services;

public interface IFileRepository : IRepository<Entities.File, FileId>
{
}
