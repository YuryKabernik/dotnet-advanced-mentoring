using CartingService.Domain.Validation;

namespace CartingService.Domain.ValueObjects;

public class Item
{
    /// <summary>
    /// Iunique identifier of the item in external system.
    /// </summary>
    required public int Id { get; set; }

    required public string Name { get; set; }
    required public decimal Price { get; set; }
    public Image? Image { get; set; }

    /// <summary>
    /// Quantity of items in the cart.
    /// </summary>
    public int Quantity { get; set; }

    public ValidationResult Validate()
    {
        if (this.Quantity < 1)
            return ValidationResult.FailedFrom(nameof(Quantity));

        if (this.Price < 0)
            return ValidationResult.FailedFrom(nameof(Price));

        return ValidationResult.Success;
    }
}
