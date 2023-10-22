namespace CartingService.Domain.Validation;

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
}
