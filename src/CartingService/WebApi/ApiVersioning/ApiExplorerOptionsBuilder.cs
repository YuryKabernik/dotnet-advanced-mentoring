using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;

namespace CartingService.WebApi.Api
{
    /// <inheritdoc/>
    public class ApiExplorerOptionsBuilder : IConfigureOptions<ApiExplorerOptions>
    {
        /// <summary>
        /// Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
        /// </summary>
        /// <param name="options"></param>
        public void Configure(ApiExplorerOptions options)
        {
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        }
    }
}
