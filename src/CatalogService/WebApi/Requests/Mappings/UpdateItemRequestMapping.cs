using Application.Items.UpdateCommand;
using CatalogService.WebApi.Endpoints;
using Riok.Mapperly.Abstractions;

namespace CatalogService.WebApi.Requests.Mappings;

[Mapper]
public static partial class UpdateItemRequestMapping
{
    public static UpdateItemCommand ToCommand(this UpdateItemRequest request, Guid itemId)
    {
        ItemUpdateValues details = request.ToUpdateValues();

        return new UpdateItemCommand(itemId, details);
    }

    private static partial ItemUpdateValues ToUpdateValues(this UpdateItemRequest request);
}