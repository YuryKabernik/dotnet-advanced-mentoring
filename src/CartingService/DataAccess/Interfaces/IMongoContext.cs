using CartingService.Abstractions.Interfaces;
using MongoDB.Driver;

namespace CartingService.DataAccess.Interfaces;

public interface IMongoContext<TEntity> where TEntity : IDocumentEntity
{
    IMongoCollection<TEntity> Collection { get; }
}
