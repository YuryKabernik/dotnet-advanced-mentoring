using Application.Items.AddCommand;
using CatalogService.WebApi.Endpoints;
using Riok.Mapperly.Abstractions;

namespace CatalogService.WebApi.Requests.Mappings;

[Mapper]
public static partial class AddItemRequestMapping
{
    public static partial AddItemCommand ToCommand(this AddItemRequest request);
}