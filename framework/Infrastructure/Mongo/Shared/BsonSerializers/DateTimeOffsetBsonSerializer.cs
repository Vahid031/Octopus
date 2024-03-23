using MongoDB.Bson.Serialization;

namespace Octopus.Infrastructure.Mongo.Shared.BsonSerializers;

internal class DateTimeOffsetBsonSerializer : IBsonSerializer<DateTimeOffset>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
    {
        context.Writer.WriteDateTime(value.ToUnixTimeMilliseconds());
    }

    public DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadDateTime();
        return DateTimeOffset.FromUnixTimeMilliseconds(value);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        Serialize(context, args, (DateTimeOffset)value);
    }

    public Type ValueType => typeof(DateTimeOffset);
}