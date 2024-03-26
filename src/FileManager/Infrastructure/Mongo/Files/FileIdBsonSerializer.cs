using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.ValueObjects;
using Octopus.FileManager.Core.Domain.Files.ValueObjects;

namespace Octopus.UserManagement.Infrastructure.Mongo.Users;

internal class FileIdBsonSerializer : IBsonSerializer<FileId>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, FileId value)
        => context.Writer.WriteString($"{value}");

    public FileId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        return FileId.Create(context.Reader.ReadString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, value is null ? null : FileId.Create($"{value}"));

    public Type ValueType => typeof(FileId);
}