using MongoDB.Bson.Serialization.Attributes;

namespace CartingService.DataAccess.ValueObjects;

public class Item
{
    /// <summary>
    /// Unique identifier of the item in external system.
    /// </summary>
    [BsonRequired]
    public required string Id { get; set; }

    [BsonRequired]
    public required string Name { get; set; }
    
    [BsonRequired]
    public required decimal Price { get; set; }
    
    public Image? Image { get; set; }
    
    public int Quantity { get; set; }
}
