using CatalogService.Application.Categories.UpdateCommand;
using Riok.Mapperly.Abstractions;

namespace CatalogService.WebApi.Requests.Mappings
{
    [Mapper]
    public static partial class UpdateCategoryRequestMapping
    {
        public static UpdateCategoryCommand ToCommand(this UpdateCategoryRequest request, Guid id)
        {
            CategoryUpdateValues details = request.ToUpdateDetails();

            return new UpdateCategoryCommand(id, details);
        }

        internal static partial CategoryUpdateValues ToUpdateDetails(this UpdateCategoryRequest request);
    }
}
