namespace CatalogService.Contracts.Interfaces;

public interface IRepository<TSource>
{
    Task Add(TSource value);
    Task Delete(string id);
    Task<TSource?> Get(string id);
    Task<IEnumerable<TSource>> Get();
    Task Update(string id, TSource value);
}
