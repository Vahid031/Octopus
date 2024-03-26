using MongoDB.Driver;
using Octopus.FileManager.Core.Domain.Files.Services;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;
using Octopus.Infrastructure.Mongo.Shared;
using File = Octopus.FileManager.Core.Domain.Files.Entities.File;

namespace Octopus.FileManagement.Infrastructure.Mongo.Files;

internal class MongoFileRepository : MongoRepositoryBase<File, FileId>, IFileRepository
{
    public const string CollectionName = "Files";
    public MongoFileRepository(IUnitOfWork unitOfWork, IMongoCollection<File> collection)
        : base(unitOfWork, collection)
    {
    }
}
