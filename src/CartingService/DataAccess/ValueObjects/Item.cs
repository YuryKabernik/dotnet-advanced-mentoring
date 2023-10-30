namespace CartingService.DataAccess.ValueObjects;

public class Item 
{
    /// <summary>
    /// Unique identifier of the item in external system.
    /// </summary>
    required public int Id { get; set; }

    required public string Name { get; set; }
    required public decimal Price { get; set; }
    public Image? Image { get; set; }

    /// <summary>
    /// Quantity of items in the cart.
    /// </summary>
    public int Quantity { get; set; }
}
