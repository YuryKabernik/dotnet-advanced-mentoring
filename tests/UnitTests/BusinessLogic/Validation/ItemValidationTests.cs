using CartingService.BusinessLogic.Validation;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.UnitTests.Validation;

public class ItemTests : Item
{
    public ItemTests()
    {
        this.Id = default;
        this.Name = string.Empty;
        this.Price = 10;
        this.Quantity = 10;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_InvalidQuantity_ValidationFailed(int quantity)
    {
        this.Quantity = quantity;
 
        var result = ValidationResult.Validate(this);
    
        Assert.False(result.IsValid);
        Assert.Equal(nameof(Quantity), result.PropertyName);
    }

    [Theory]
    [InlineData(-0.01)]
    public void Validate_InvalidPrice_ValidationFailed(decimal price)
    {
        this.Price = price;
     
        var result = ValidationResult.Validate(this);
    
        Assert.False(result.IsValid);
        Assert.Equal(nameof(Price), result.PropertyName);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(10, 10)]
    public void Validate_ValidPriceQuantity_ValidationSucceeded(int quantity, decimal price)
    {
        this.Quantity = quantity;
        this.Price = price;

        var result = ValidationResult.Validate(this);

        Assert.True(result.IsValid);
        Assert.Null(result.PropertyName);
    }
}