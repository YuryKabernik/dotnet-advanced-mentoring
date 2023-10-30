using CartingService.DataAccess.Etities;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.Settings;
using CartingService.DataAccess.ValueObjects;
using MongoDB.Driver;

namespace CartingService.DataAccess;

public class CartContext : ICartRepository
{
    private readonly MongoClient client;
    private readonly IMongoDatabase database;

    public CartContext(RepositorySettings settings)
    {
        this.client = new MongoClient(settings.ConnectionString);
        this.database = this.client.GetDatabase(settings.DatabaseName);
    }

    public ICartEntity GetCart(Guid guid)
    {
        string name = guid.ToString();
        IMongoCollection<Item> collection = database.GetCollection<Item>(name);

        return new Cart(guid, collection);
    }
}
