using CartingService.BusinessLogic.Commands;
using CartingService.BusinessLogic.Exceptions;
using CartingService.DataAccess.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

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
    public async void Execute_OnFailedRemoval_ThrowsCommandException()
    {
        // Given
        var itemId = 100;
        var guid = Guid.NewGuid();
        var request = new RemoveRequest(guid, itemId);
        var expectedErrorMessage = $"Item <{request.ItemId}> deletion failed.";

        this.cartMock.Remove(itemId).Returns(false);
        this.cartRepositoryMock.GetCart(guid).Returns(this.cartMock);

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

        this.cartMock.Remove(itemId).Returns(true);
        this.cartRepositoryMock.GetCart(guid).Returns(cartMock);

        // When
        await this.command.Execute(request, cancellationToken);

        // Then
        this.cartRepositoryMock.Received(1).GetCart(guid);
        await this.cartMock.Received(1).Remove(itemId);
    }
}
