namespace CartingService.Domain.Validation;

public struct ValidationResult
{
    public ValidationResult(bool isValid, string name)
    {
        this.IsValid = isValid;
        this.PropertyName = name;
    }

    public bool IsValid { get; }
    public string? PropertyName { get; }

    public static readonly ValidationResult Success = new(true, string.Empty);
    public static ValidationResult FailedFrom(string propertyName) => new(false, propertyName);
}
