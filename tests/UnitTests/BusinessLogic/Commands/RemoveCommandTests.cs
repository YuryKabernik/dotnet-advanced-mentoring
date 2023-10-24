using CartingService.BusinessLogic.Commands;
using CartingService.BusinessLogic.Exceptions;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Interfaces.Ports;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;

namespace CartingService.BusinessLogic.UnitTests;

public class RemoveCommandTests
{
    private readonly ICartEntity cartMock;
    private readonly ICartRepository cartRepositoryMock;
    private readonly RemoveCommand command;

    public RemoveCommandTests()
    {
        this.cartMock = Substitute.For<ICartEntity>();
        this.cartRepositoryMock = Substitute.For<ICartRepository>();
        this.command = new RemoveCommand(this.cartRepositoryMock);
    }

    [Fact]
    public async void Execute_WithEmptyGuid_ThrowsArgumentException()
    {
        // Given
        var guid = Guid.Empty;
        var request = new RemoveRequest(guid, default);
        var expectedErrorMessage = $"Empty Guid is not allowed to lookup the cart.";

        // When
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => this.command.Execute(request, CancellationToken.None)
        );

        // Then
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async void Execute_OnNotExistingCart_ThrowsCartLookupException()
    {
        // Given
        var guid = Guid.NewGuid();
        var request = new RemoveRequest(guid, default);
        var expectedErrorMessage = $"Cart <{guid}> lookup failed.";

        this.cartRepositoryMock.GetCart(guid).ReturnsNull();

        // When
        var exception = await Assert.ThrowsAsync<CartLookupException>(
            () => this.command.Execute(request, CancellationToken.None)
        );

        // Then
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async void Execute_OnFailedRemoval_ThrowsCommandException()
    {
        // Given
        var itemId = 100;
        var guid = Guid.NewGuid();
        var request = new RemoveRequest(guid, itemId);
        var expectedErrorMessage = $"Item <{request.ItemId}> deletion failed.";

        cartMock.Remove(itemId).Returns(false);
        cartRepositoryMock.GetCart(guid).Returns(cartMock);

        // When
        var exception = await Assert.ThrowsAsync<CommandFailedException>(
            () => this.command.Execute(request, CancellationToken.None)
        );

        // Then
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async void Execute_OperationCompleted_RemovesItem()
    {
        // Given
        var itemId = 101;
        var guid = Guid.NewGuid();
        var request = new RemoveRequest(guid, itemId);
        var cancellationToken = new CancellationTokenSource().Token;

        cartMock.Remove(itemId).Returns(true);
        cartRepositoryMock.GetCart(guid).Returns(cartMock);

        // When
        await this.command.Execute(request, cancellationToken);

        // Then
        await this.cartRepositoryMock.Received(1).SaveChanges(cancellationToken);
    }
}
