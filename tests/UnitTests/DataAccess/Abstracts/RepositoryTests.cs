using CartingService.DataAccess.Abstractions;
using CartingService.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;

namespace CartingService.DataAccess.UnitTests.Entities;

public class RepositoryTests
{
    private readonly Entity listedEntity;
    private readonly Entity unlistedEntity;

    private readonly IMongoCollection<Entity> collectionMock;
    private readonly Repository<Entity> cartRepository;
    private readonly IAsyncCursor<Entity> entityCursor;
    private readonly IAsyncCursor<BsonDocument> bsonCursor;

    public RepositoryTests()
    {
        this.listedEntity = Substitute.For<Entity>();
        this.unlistedEntity = Substitute.For<Entity>();

        this.entityCursor = Substitute.For<IAsyncCursor<Entity>>();
        this.bsonCursor = Substitute.For<IAsyncCursor<BsonDocument>>();

        this.collectionMock = Substitute.For<IMongoCollection<Entity>>();

        var context = Substitute.For<IMongoContext<Entity>>();
        context.Collection.Returns(this.collectionMock);

        this.cartRepository = new Repository<Entity>(context);
    }

    [Fact]
    public async Task GetById_EntityInList_ReturnsEntity()
    {
        var filterResult = new List<Entity> { this.listedEntity };
        var enumerator = filterResult.GetEnumerator();

        this.collectionMock
            .FindAsync<Entity>(FilterDefinition<Entity>.Empty, default, default)
            .ReturnsForAnyArgs(this.entityCursor);

        this.entityCursor.Current.Returns(filterResult);
        this.entityCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => enumerator.MoveNext());

        var result = await this.cartRepository.GetAsync(this.listedEntity.RawId);

        Assert.NotNull(result);
        Assert.Equal(this.listedEntity.PrimaryId, result.PrimaryId);
        Assert.Equal(this.listedEntity.RawId, result.RawId);
    }

    [Fact]
    public async Task GetById_EntityNotInList_ReturnsNull()
    {
        var filterResult = Enumerable.Empty<Entity>();

        this.collectionMock
            .FindAsync<Entity>(FilterDefinition<Entity>.Empty, default, default)
            .ReturnsForAnyArgs(this.entityCursor);
        this.entityCursor.Current.Returns(filterResult);
        this.entityCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(false);

        var result = await this.cartRepository.GetAsync(this.unlistedEntity.RawId);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_EntityIsInList_ReturnsFalse()
    {
        var filterResult = new List<BsonDocument> { new() };

        this.collectionMock
            .FindAsync<BsonDocument>(FilterDefinition<Entity>.Empty, default, default)
            .ReturnsForAnyArgs(this.bsonCursor);
        this.bsonCursor.Current.Returns(filterResult);
        this.bsonCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(true);

        // Act
        await this.cartRepository.AddAsync(this.listedEntity);

        // Assert
        await this.collectionMock.ReceivedWithAnyArgs(Quantity.Exactly(1)).InsertOneAsync(this.listedEntity);
    }

    [Fact]
    public async Task AddAsync_EntityIsNotInList_AddsEntity()
    {
        var filterResult = Enumerable.Empty<BsonDocument>();

        this.collectionMock
            .FindAsync<BsonDocument>(FilterDefinition<Entity>.Empty, default, default)
            .ReturnsForAnyArgs(this.bsonCursor);
        this.bsonCursor.Current.Returns(filterResult);
        this.bsonCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => Task.FromResult(false));

        // Act
        await this.cartRepository.AddAsync(this.listedEntity);

        // Assert
        await this.collectionMock.Received(1).InsertOneAsync(this.listedEntity);
    }

    [Fact]
    public async Task RemoveAsync_EntityIsNotInList_ReturnsFalse()
    {
        this.collectionMock
            .FindOneAndDeleteAsync(
                FilterDefinition<Entity>.Empty,
                Arg.Any<FindOneAndDeleteOptions<Entity, Entity>>(),
                CancellationToken.None
            )
            .ReturnsNullForAnyArgs();

        // Act
        await this.cartRepository.RemoveAsync(this.unlistedEntity.RawId);

        // Assert
        await this.collectionMock.Received(1).FindOneAndDeleteAsync(
            Arg.Any<FilterDefinition<Entity>>(),
            Arg.Any<FindOneAndDeleteOptions<Entity, Entity>>(),
            CancellationToken.None
        );
    }

    [Fact]
    public async Task RemoveAsync_EntityIsInList_RemovesEntity()
    {
        this.collectionMock
            .FindOneAndDeleteAsync(
                FilterDefinition<Entity>.Empty,
                Arg.Any<FindOneAndDeleteOptions<Entity, Entity>>(),
                CancellationToken.None
            )
            .ReturnsForAnyArgs(this.listedEntity);

        // Act
        await this.cartRepository.RemoveAsync(this.listedEntity.RawId);

        // Assert
        await this.collectionMock.Received(1).FindOneAndDeleteAsync(
            Arg.Any<FilterDefinition<Entity>>(),
            Arg.Any<FindOneAndDeleteOptions<Entity, Entity>>(),
            CancellationToken.None
        );
    }

    [Fact]
    public async Task GetAsync_CollectionIsEmpty_ReturnsEmptyList()
    {
        var filterResult = Enumerable.Empty<Entity>();

        this.collectionMock
            .FindAsync<Entity>(FilterDefinition<Entity>.Empty)
            .ReturnsForAnyArgs(this.entityCursor);

        this.entityCursor.Current.Returns(filterResult);
        this.entityCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(false);

        // Act
        var resultList = await this.cartRepository.GetAsync();

        // Assert
        Assert.Empty(resultList);
    }

    [Fact]
    public async Task GetAsync_WithEntitys_ReturnsList()
    {
        var filterResult = new List<Entity> { this.listedEntity };
        var enumerator = filterResult.GetEnumerator();

        this.collectionMock
            .FindAsync<Entity>(FilterDefinition<Entity>.Empty, default, default)
            .ReturnsForAnyArgs(this.entityCursor);

        this.entityCursor.Current.Returns(filterResult);
        this.entityCursor.MoveNextAsync(Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => enumerator.MoveNext());

        // Act
        var resultList = await this.cartRepository.GetAsync();

        // Assert
        Assert.Single(resultList);
    }
}
