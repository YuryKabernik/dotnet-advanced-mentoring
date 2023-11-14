using CartingService.Abstractions.Interfaces;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CartingService.DataAccess;

public class MongoContext<TEntity> : IMongoContext<TEntity> where TEntity : IDocumentEntity
{
    private readonly IMongoDatabase database;
    private readonly IOptions<RepositorySettings> options;

    public MongoContext(IOptions<RepositorySettings> options)
    {
        MongoClient client = new MongoClient(options.Value.ConnectionString);
        this.database = client.GetDatabase(options.Value.DatabaseName);
        this.options = options;
    }

    public IMongoCollection<TEntity> Collection => database.GetCollection<TEntity>(options.Value.CollectionName);
}
