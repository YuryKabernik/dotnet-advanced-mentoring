using CartingService.BusinessLogic.Validation;

namespace CartingService.BusinessLogic.UnitTests.Validation;

public class ValidationResultTests
{
    [Fact]
    public void Success_StaticField_IsValidResult()
    {
        Assert.True(ValidationResult.Success.IsValid);
        Assert.Null(ValidationResult.Success.PropertyName);
    }

    [Fact]
    public void FailedFrom_PropertyName_IsValidResult()
    {
        var expected = "CustomPropertyName";

        var result = ValidationResult.FailedFrom(expected);

        Assert.False(result.IsValid);
        Assert.Equal(expected, result.PropertyName);
    }
}
