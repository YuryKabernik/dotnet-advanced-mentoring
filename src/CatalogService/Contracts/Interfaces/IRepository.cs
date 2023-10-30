namespace CatalogService.Contracts.Interfaces;

public interface IRepository<TSource>
{
    Task Add(TSource value);
    Task Delete(int id);
    Task<TSource?> Get(int id);
    Task<IEnumerable<TSource>> List();
    Task Update(int id, TSource newValue);
}
