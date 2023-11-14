using MongoDB.Bson.Serialization.Attributes;

namespace CartingService.DataAccess.ValueObjects;

public class Item 
{
    /// <summary>
    /// Unique identifier of the item in external system.
    /// </summary>
    [BsonRequired]
    required public string Id { get; set; }

    [BsonRequired]
    required public string Name { get; set; }
    
    [BsonRequired]
    required public decimal Price { get; set; }
    
    public Image? Image { get; set; }
    
    public int Quantity { get; set; }
}
