using CartingService.DataAccess.Etities;
using CartingService.DataAccess.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CartingService.DataAccess.UnitTests.Entities;

public class CartTests : Cart
{
    private readonly Item listedItem = new() { Id = 100, Name = "Title", Price = 10, Quantity = 10 };
    private readonly Item unlistedItem = new() { Id = 101, Name = string.Empty, Price = default, Quantity = 1 };

    private readonly IMongoCollection<Item> collectionMock;
    private readonly IAsyncCursor<Item> itemCursor;
    private readonly IAsyncCursor<BsonDocument> bsonCursor;

    public CartTests() : base(default, default!)
    {
        this.itemCursor = Substitute.For<IAsyncCursor<Item>>();
        this.bsonCursor = Substitute.For<IAsyncCursor<BsonDocument>>();
        this.collectionMock = Substitute.For<IMongoCollection<Item>>();

        this.Guid = new Guid();
        this.Items = this.collectionMock;
    }

    [Fact]
    public async Task Get_ItemInList_ReturnsItem()
    {
        var filterResult = new List<Item> { this.listedItem };
        var enumerator = filterResult.GetEnumerator();

        this.collectionMock
            .FindAsync<Item>(FilterDefinition<Item>.Empty, default, default)
            .ReturnsForAnyArgs(this.itemCursor);

        this.itemCursor.Current.Returns(filterResult);
        this.itemCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => enumerator.MoveNext());

        var result = await this.Get(this.listedItem.Id);

        Assert.NotNull(result);
        Assert.Equal(this.listedItem.Id, result.Id);
        Assert.Equal(this.listedItem.Name, result.Name);
        Assert.Equal(this.listedItem.Price, result.Price);
        Assert.Equal(this.listedItem.Quantity, result.Quantity);
    }

    [Fact]
    public async Task Get_ItemNotInList_ReturnsNull()
    {
        var filterResult = Enumerable.Empty<Item>();

        this.collectionMock
            .FindAsync<Item>(FilterDefinition<Item>.Empty, default, default)
            .ReturnsForAnyArgs(this.itemCursor);
        this.itemCursor.Current.Returns(filterResult);
        this.itemCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(false);

        var result = await this.Get(this.unlistedItem.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task Add_ItemIsInList_ReturnsFalse()
    {
        var filterResult = new List<BsonDocument> { new() };

        this.collectionMock
            .FindAsync<BsonDocument>(FilterDefinition<Item>.Empty, default, default)
            .ReturnsForAnyArgs(this.bsonCursor);
        this.bsonCursor.Current.Returns(filterResult);
        this.bsonCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(true);

        bool isAdded = await this.Add(this.listedItem);

        Assert.False(isAdded);
    }

    [Fact]
    public async Task Add_ItemIsNotInList_AddsItem()
    {
        var filterResult = Enumerable.Empty<BsonDocument>();

        this.collectionMock
            .FindAsync<BsonDocument>(FilterDefinition<Item>.Empty, default, default)
            .ReturnsForAnyArgs(this.bsonCursor);
        this.bsonCursor.Current.Returns(filterResult);
        this.bsonCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => Task.FromResult(false));

        bool isAdded = await this.Add(this.unlistedItem);

        Assert.True(isAdded);
        await this.collectionMock.Received(1).InsertOneAsync(this.unlistedItem);
    }

    [Fact]
    public async Task Remove_ItemIsNotInList_ReturnsFalse()
    {
        this.collectionMock
            .FindOneAndDeleteAsync(FilterDefinition<Item>.Empty, Arg.Any<FindOneAndDeleteOptions<Item, Item>>(), CancellationToken.None)
            .ReturnsNullForAnyArgs();

        bool isRemoved = await this.Remove(this.unlistedItem.Id);

        Assert.False(isRemoved);
    }

    [Fact]
    public async Task Remove_ItemIsInList_RemovesItem()
    {
        this.collectionMock
            .FindOneAndDeleteAsync(FilterDefinition<Item>.Empty, Arg.Any<FindOneAndDeleteOptions<Item, Item>>(), CancellationToken.None)
            .ReturnsForAnyArgs(this.listedItem);

        bool isRemoved = await this.Remove(this.listedItem.Id);

        Assert.True(isRemoved);
    }

    [Fact]
    public async Task List_IsEmpty_ReturnsEmptyList()
    {
        var filterResult = Enumerable.Empty<Item>();

        this.collectionMock
            .FindAsync<Item>(FilterDefinition<Item>.Empty)
            .ReturnsForAnyArgs(this.itemCursor);

        this.itemCursor.Current.Returns(filterResult);
        this.itemCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(false);

        var resultList = await this.List();

        Assert.Empty(resultList);
    }

    [Fact]
    public async Task List_WithItems_ReturnsList()
    {
        var filterResult = new List<Item> { this.listedItem };
        var enumerator = filterResult.GetEnumerator();

        this.collectionMock
            .FindAsync<Item>(FilterDefinition<Item>.Empty, default, default)
            .ReturnsForAnyArgs(this.itemCursor);

        this.itemCursor.Current.Returns(filterResult);
        this.itemCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => enumerator.MoveNext());

        var resultList = await this.List();

        Assert.Single(resultList);
    }
}
