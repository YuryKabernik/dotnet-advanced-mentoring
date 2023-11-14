﻿using CartingService.Abstractions.Interfaces;
using CartingService.DataAccess.Interfaces;
using MongoDB.Driver;

namespace CartingService.DataAccess.Abstractions;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private IMongoContext<TEntity> context;

    public Repository(IMongoContext<TEntity> context) => this.context = context;

    public async Task<IEnumerable<TEntity>> GetAsync() => await this.context.Collection.Find(_ => true).ToListAsync();

    public async Task<TEntity> GetAsync(string id) => await this.context.Collection.Find(entry => entry.RawId.Equals(id, StringComparison.Ordinal)).FirstOrDefaultAsync();

    public async Task AddAsync(TEntity item) => await this.context.Collection.InsertOneAsync(item);

    public async Task RemoveAsync(string id) => await this.context.Collection.DeleteOneAsync(entry => entry.RawId.Equals(id, StringComparison.Ordinal));

    public async Task UpdateAsync(TEntity item) => await this.context.Collection.ReplaceOneAsync(entry => entry.RawId.Equals(item.RawId, StringComparison.Ordinal), item);
}