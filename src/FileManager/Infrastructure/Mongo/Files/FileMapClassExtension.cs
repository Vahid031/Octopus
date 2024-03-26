using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.Entities;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;
using File = Octopus.FileManager.Core.Domain.Files.Entities.File;

namespace Octopus.UserManagement.Infrastructure.Mongo.Users;

internal class FileMapClassExtension
{
    internal static void Register()
    {
        BsonSerializer.RegisterSerializer(new FileIdBsonSerializer());

        BsonClassMap.RegisterClassMap<EntityBase<FileId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<File>(cm =>
        {
            cm.MapMember(m => m.RelativePath).SetElementName("RelativePath");
            cm.MapMember(m => m.CreatedBy).SetElementName("CreatedBy");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.Length).SetElementName("Length");
            cm.MapMember(m => m.OriginalName).SetElementName("OriginalName");
            cm.MapMember(m => m.Type).SetElementName("Type");

            cm.SetIgnoreExtraElements(true);
        });
    }
}