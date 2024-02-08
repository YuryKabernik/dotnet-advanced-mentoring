﻿using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.Domain.Entities;

public class Item : Entity<Guid>
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public Uri? Image { get; set; }

    public required Category Category { get; set; }

    public required decimal Price { get; set; }

    public required int Amount { get; set; }
}
