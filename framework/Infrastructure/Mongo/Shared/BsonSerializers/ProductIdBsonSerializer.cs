﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.ValueObjects;

namespace Octopus.Infrastructure.Mongo.Shared.BsonSerializers;
internal class ProductIdBsonSerializer : IBsonSerializer<ProductId>
{
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        => Deserialize(context, args);

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ProductId value)
        => context.Writer.WriteString($"{value}");

    public ProductId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        if (context.Reader.CurrentBsonType == BsonType.Null)
        {
            context.Reader.ReadNull();
            return null;
        }

        return ProductId.Create(context.Reader.ReadString());
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        => Serialize(context, args, value is null ? null : ProductId.Create($"{value}"));

    public Type ValueType => typeof(ProductId);
}
