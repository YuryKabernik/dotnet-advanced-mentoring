using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.ValueObjects;
using MongoDB.Driver;

namespace CartingService.DataAccess.Etities;

public class Cart : ICartEntity
{
    public Guid Guid { get; set; }
    public IMongoCollection<Item> Items { get; set; }

    public Cart(Guid guid, IMongoCollection<Item> items)
    {
        this.Guid = guid;
        this.Items = items;
    }

    public async Task<Item?> Get(int itemId)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.Id, itemId);
        return await this.Items.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> Add(Item item)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.Id, item.Id);
        bool hasAnyMatch = await this.Items.Find(filter).AnyAsync();
        bool canBeAdded = !hasAnyMatch;

        if (canBeAdded)
        {
            await this.Items.InsertOneAsync(item);
        }

        return canBeAdded;
    }

    public async Task<bool> Remove(int itemId)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.Id, itemId);
        var deletedItem = await this.Items.FindOneAndDeleteAsync(filter);

        return deletedItem is not null;
    }

    public async Task<IEnumerable<Item>> List()
    {
        var findFluent = this.Items.Find(FilterDefinition<Item>.Empty);

        return await findFluent.ToListAsync();
    }

    public async Task Update(Item item)
    {
        var filter = Builders<Item>.Filter.Eq(i => i.Id, item.Id);

        await this.Items.FindOneAndReplaceAsync(filter, item);
    }
}
