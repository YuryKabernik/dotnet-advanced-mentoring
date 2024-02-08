namespace CatalogService.Domain.Contracts.Entities;

public abstract class Entity<TEntityId> 
{
    public required TEntityId Id { get; set; }
}