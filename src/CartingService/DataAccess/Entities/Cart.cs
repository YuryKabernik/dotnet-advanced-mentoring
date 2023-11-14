using CartingService.DataAccess.Abstractions;
using CartingService.DataAccess.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;

namespace CartingService.DataAccess.Entities;

public class Cart : Entity
{
    [BsonElement]
    public ISet<Item> Items { get; set; } = null!;
}
