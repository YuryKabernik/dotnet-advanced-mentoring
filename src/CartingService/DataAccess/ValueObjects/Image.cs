namespace CartingService.DataAccess.ValueObjects;

public class Image
{
    public required Uri Url { get; set; }
    public required string AltText { get; set; }
}