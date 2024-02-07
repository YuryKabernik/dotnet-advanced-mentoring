namespace CartingService.WebApi.Requests;

/// <summary>
/// Request contract for adding new items to the cart
/// </summary>
public class CartNewItem
{
    /// <summary>
    /// Unique identity of the item.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Quantity of items to add
    /// </summary>
    public required int Quantity { get; set; }
}