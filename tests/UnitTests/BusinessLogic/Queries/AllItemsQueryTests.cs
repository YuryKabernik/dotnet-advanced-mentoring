using CartingService.Abstractions.Interfaces;
using CartingService.DataAccess.ValueObjects;
using CartingService.DataAccess.Entities;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using CartingService.BusinessLogic.Queries.Items;

namespace CartingService.BusinessLogic.UnitTests;

public class AllItemsQueryTests
{
    private readonly IRepository<Cart> cartRepositoryMock;
    private ItemsQueryHandler query;
    private Cart cartMock;

    public AllItemsQueryTests()
    {
        this.cartMock = Substitute.For<Cart>();
        this.cartMock.Items = Substitute.For<IDictionary<string, Item>>();

        this.cartRepositoryMock = Substitute.For<IRepository<Cart>>();

        this.query = new ItemsQueryHandler(this.cartRepositoryMock);
    }

    [Fact]
    public async void Execute_WithEmptyGuid_ThrowsArgumentException()
    {
        var guid = Guid.Empty.ToString();
        var expectedErrorMessage = $"Object reference not set to an instance of an object.";
        var request = new ItemsQueryRequest(guid);

        var exception = await Assert.ThrowsAsync<NullReferenceException>(
            () => this.query.Handle(request, CancellationToken.None)
        );

        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async Task Execute_OnExistingCart_ListsItems()
    {
        // Given
        var guid = Guid.NewGuid().ToString();
        var request = new ItemsQueryRequest(guid);
        var expectedItems = new List<Item>();

        this.cartRepositoryMock.GetAsync(guid).Returns(this.cartMock);
        this.cartMock.Items.Values.Returns(expectedItems);

        // When
        var result = await query.Handle(request, CancellationToken.None);

        // Then
        Assert.Equal(expectedItems, result.Items);
        await this.cartRepositoryMock.Received(Quantity.Exactly(1)).GetAsync(guid);
    }
}
