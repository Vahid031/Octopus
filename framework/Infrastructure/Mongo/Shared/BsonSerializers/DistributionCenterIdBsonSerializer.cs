using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Infrastructure.Mongo.Shared.BsonSerializers;

internal class DistributionCenterIdBsonSerializer : IBsonSerializer<DistributionCenterId>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DistributionCenterId value)
        => context.Writer.WriteString($"{value}");

    public DistributionCenterId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        return DistributionCenterId.Create(context.Reader.ReadString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, value is null ? null : DistributionCenterId.Create($"{value}"));

    public Type ValueType => typeof(DistributionCenterId);
}
