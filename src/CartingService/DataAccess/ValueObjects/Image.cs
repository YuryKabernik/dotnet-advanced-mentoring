using MongoDB.Bson.Serialization.Attributes;

namespace CartingService.DataAccess.ValueObjects;

public class Image
{
    [BsonRequired]
    public required Uri Url { get; set; }
    
    [BsonRequired]
    public required string AltText { get; set; }
}