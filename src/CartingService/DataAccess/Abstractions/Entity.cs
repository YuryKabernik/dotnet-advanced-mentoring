using CartingService.Abstractions.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CartingService.DataAccess.Abstractions;

public abstract class Entity : IDocumentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string PrimaryId { get; set; }

    [BsonElement("raw_id")]
    [BsonRepresentation(BsonType.String)]
    public required string RawId { get; set; }
}