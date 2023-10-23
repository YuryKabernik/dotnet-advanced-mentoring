using NSubstitute;
using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Queries;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Interfaces.Ports;
using CartingService.Domain.ValueObjects;
using NSubstitute.ReturnsExtensions;
using NSubstitute.ReceivedExtensions;

namespace CartingService.BusinessLogic.UnitTests;

public class AllItemsQueryTests
{
    private AllItemsQuery query;
    private ICartRepository cartRepositoryMock;
    private ICartEntity cartMock;

    public AllItemsQueryTests()
    {
        this.cartMock = Substitute.For<ICartEntity>();
        this.cartRepositoryMock = Substitute.For<ICartRepository>();
        this.query = new AllItemsQuery(cartRepositoryMock);
    }

    [Fact]
    public void Execute_WithEmptyGuid_ThrowsArgumentException()
    {
        var guid = Guid.Empty;
        var expectedErrorMessage = $"Empty Guid is not allowed to lookup the cart.";
        var request = new ItemsRequest(guid);

        var task = query.Execute(request, CancellationToken.None);

        Assert.ThrowsAsync<ArgumentException>(() => task);
        Assert.Equal(expectedErrorMessage, task.Exception?.InnerException?.Message);
    }

    [Fact]
    public void Execute_OnNotExistingCart_ThrowsCartLookupException()
    {
        // Given
        var guid = Guid.NewGuid();
        var expectedErrorMessage = $"Cart <{guid}> lookup failed.";
        var request = new ItemsRequest(guid);

        this.cartRepositoryMock.GetCart(guid).ReturnsNull();

        // When
        var task = query.Execute(request, CancellationToken.None);

        // Then
        Assert.ThrowsAsync<CartLookupException>(() => task);
        Assert.Equal(expectedErrorMessage, task.Exception?.InnerException?.Message);
    }

    [Fact]
    public async void Execute_OnExistingCart_ListsItems()
    {
        // Given
        var guid = Guid.NewGuid();
        var request = new ItemsRequest(guid);
        var expectedItems = new List<Item>();

        this.cartMock.List().Returns(expectedItems);
        this.cartRepositoryMock.GetCart(guid).Returns(cartMock);

        // When
        var result = await query.Execute(request, CancellationToken.None);

        // Then
        Assert.Equal(expectedItems, result);
        this.cartMock.Received(Quantity.Exactly(1)).List();
    }
}
