using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Mongo.Users;
internal class UserIdBsonSerializer : IBsonSerializer<UserId>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, UserId value)
        => context.Writer.WriteString($"{value}");

    public UserId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        return UserId.Create(context.Reader.ReadString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, value is null ? null : UserId.Create($"{value}"));

    public Type ValueType => typeof(UserId);
}
