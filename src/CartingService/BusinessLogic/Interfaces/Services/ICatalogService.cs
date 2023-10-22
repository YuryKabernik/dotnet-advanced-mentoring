﻿using CartingService.Domain.ValueObjects;

namespace CartingService.BusinessLogic.Interfaces.Services;

public interface ICatalogService
{
    Task<Item> GetItem(int id);
}