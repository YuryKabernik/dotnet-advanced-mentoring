using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Commands;
using CartingService.BusinessLogic.Exceptions;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace CartingService.BusinessLogic.UnitTests;

public class RemoveCommandTests
{
    private readonly Cart cartMock;
    private readonly ILogger<RemoveCommand> loggerMock;
    private readonly IRepository<Cart> cartRepositoryMock;
    private readonly RemoveCommand command;

    public RemoveCommandTests()
    {
        this.cartMock = Substitute.For<Cart>();
        this.cartMock.Items = Substitute.For<IDictionary<string, Item>>();

        this.loggerMock = Substitute.For<ILogger<RemoveCommand>>();
        this.cartRepositoryMock = Substitute.For<IRepository<Cart>>();

        this.command = new RemoveCommand(this.cartRepositoryMock, this.loggerMock);
    }

    [Fact]
    public async void Execute_WithEmptyGuid_ThrowsArgumentException()
    {
        // Given
        var guid = Guid.Empty.ToString();
        var request = new RemoveRequest(guid, default);
        var expectedErrorMessage = $"Object reference not set to an instance of an object.";

        // When
        var exception = await Assert.ThrowsAsync<NullReferenceException>(
            () => this.command.Execute(request, CancellationToken.None)
        );

        // Then
        Assert.Equal(expectedErrorMessage, exception.Message);
    }

    [Fact]
    public async void Execute_OnFailedRemoval_ThrowsCommandException()
    {
        // Given
        var itemId = "100";
        var guid = Guid.NewGuid().ToString();
        var request = new RemoveRequest(guid, itemId);
        var expectedErrorMessage = $"Item '{request.ItemId}' deletion failed.";

        this.cartMock.Items.Remove(Arg.Any<string>()).Returns(false);
        this.cartRepositoryMock.GetAsync(guid).Returns(this.cartMock);

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
        var itemId = "101";
        var guid = Guid.NewGuid().ToString();
        var request = new RemoveRequest(guid, itemId);
        var cancellationToken = new CancellationTokenSource().Token;

        this.cartMock.Items.Remove(Arg.Any<string>()).Returns(true);
        this.cartRepositoryMock.GetAsync(guid).Returns(this.cartMock);

        // When
        await this.command.Execute(request, cancellationToken);

        // Then
        await this.cartRepositoryMock.Received(1).GetAsync(guid);
    }
}
