using CartingService.DataAccess.Etities;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.DataAccess.UnitTests.Entities;

public class CartTests : Cart
{
    private readonly Item invalidItem = new() { Id = default, Name = string.Empty, Price = default, Quantity = default };
    private readonly Item listedItem = new() { Id = 100, Name = "Title", Price = 10, Quantity = 10 };
    private readonly Item unlistedItem = new() { Id = 101, Name = string.Empty, Price = default, Quantity = 1 };

    public CartTests()
    {
        this.Guid = new Guid();
        this.Items = new Dictionary<int, Item>() {
            { listedItem.Id, listedItem }
        };
    }

    [Fact]
    public void Get_ItemInList_ReturnsItem()
    {
        var result = this.Get(listedItem.Id);

        Assert.NotNull(result);
        Assert.Equal(listedItem.Id, result.Id);
        Assert.Equal(listedItem.Name, result.Name);
        Assert.Equal(listedItem.Price, result.Price);
        Assert.Equal(listedItem.Quantity, result.Quantity);
    }

    [Fact]
    public void Get_ItemNotInList_ReturnsNull()
    {
        var result = this.Get(unlistedItem.Id);

        Assert.Null(result);
    }

    [Fact]
    public void Add_ItemIsInList_ReturnsFalse()
    {
        bool isAdded = this.Add(listedItem);

        Assert.False(isAdded);
        Assert.Single(this.Items);
        Assert.DoesNotContain(unlistedItem.Id, Items.Keys);
    }

    [Fact]
    public void Add_ItemIsNotInList_AddsItem()
    {
        bool isAdded = this.Add(unlistedItem);

        Assert.True(isAdded);
        Assert.True(this.Items.Count == 2);
        Assert.Contains(unlistedItem.Id, Items.Keys);
    }

    [Fact]
    public void Remove_ItemIsNotInList_ReturnsFalse()
    {
        bool isRemoved = this.Remove(unlistedItem.Id);

        Assert.False(isRemoved);
        Assert.Single(this.Items);
        Assert.Contains(listedItem.Id, Items.Keys);
    }

    [Fact]
    public void Remove_ItemIsInList_RemovesItem()
    {
        bool isRemoved = this.Remove(listedItem.Id);

        Assert.True(isRemoved);
        Assert.Empty(this.Items);
    }

    [Fact]
    public void List_NullableCollection_ReturnsEmptyList()
    {
        this.Items = null;

        var resultList = this.List();

        Assert.Empty(resultList);
    }

    [Fact]
    public void List_IsEmpty_ReturnsEmptyList()
    {
        this.Items = new Dictionary<int, Item>();

        var resultList = this.List();

        Assert.Empty(resultList);
    }

    [Fact]
    public void List_WithItems_ReturnsList()
    {
        var resultList = this.List();

        Assert.Single(resultList);
    }
}
