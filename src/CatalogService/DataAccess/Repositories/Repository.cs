using CatalogService.Contracts.Interfaces;
using LinqToDB;

namespace CatalogService.DataAccess.Repositories;

public class Repository<TEntry> : IRepository<TEntry> where TEntry : class, ITableModel
{
    private readonly DbContext<TEntry> context;

    public Repository(DbContext<TEntry> context)
    {
        this.context = context;
    }

    public async Task<TEntry?> Get(string id) => await this.context.Table.FirstOrDefaultAsync(row => row.Id == id);

    public async Task<IEnumerable<TEntry>> Get() => await this.context.Table.ToListAsync();

    public async Task Add(TEntry item) => await this.context.InsertOrReplaceAsync(item);

    public async Task Delete(string id)
    {
        TEntry? entry = await this.Get(id);

        if (entry is not null)
        {
            await this.context.DeleteAsync(entry);
        }
    }

    public async Task Update(string id, TEntry newEntry)
    {
        bool tableContainsItem = await this.context.Table.ContainsAsync(newEntry);

        if (newEntry.Id == id && tableContainsItem)
        {
            await this.context.UpdateAsync(newEntry);
        }
    }
}
