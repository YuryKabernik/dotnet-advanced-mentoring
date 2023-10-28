using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Validation;

public struct ValidationResult
{
    public static readonly ValidationResult Success = new(true, null);
    public static ValidationResult FailedFrom(string propertyName) => new(false, propertyName);

    internal ValidationResult(bool isValid, string? name)
    {
        this.IsValid = isValid;
        this.PropertyName = name;
    }

    public bool IsValid { get; }
    public string? PropertyName { get; }


    public static ValidationResult Validate(Item item)
    {
        if (item.Quantity < 1)
            return ValidationResult.FailedFrom(nameof(item.Quantity));

        if (item.Price < 0)
            return ValidationResult.FailedFrom(nameof(item.Price));

        return ValidationResult.Success;
    }
}
