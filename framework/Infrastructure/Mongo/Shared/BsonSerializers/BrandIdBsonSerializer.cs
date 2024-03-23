using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Infrastructure.Mongo.Shared.BsonSerializers;

internal class BrandIdBsonSerializer : IBsonSerializer<BrandId>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BrandId value)
        => context.Writer.WriteString($"{value}");

    public BrandId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        return BrandId.Create(context.Reader.ReadString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, value is null ? null : DistributionCenterId.Create($"{value}"));

    public Type ValueType => typeof(BrandId);
}