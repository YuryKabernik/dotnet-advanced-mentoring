using CatalogService.Application.Categories.AddCommand;
using Riok.Mapperly.Abstractions;

namespace CatalogService.WebApi.Requests.Mappings
{
    [Mapper]
    public static partial class AddCategoryRequestMapping
    {
        public static partial AddCategoryCommand ToCommand(this AddCategoryRequest request);
    }
}
