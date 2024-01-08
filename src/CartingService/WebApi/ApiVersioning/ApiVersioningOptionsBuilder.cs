using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace CartingService.WebApi.Api
{
    /// <summary>
    /// Configure Api versioning
    /// </summary>
    public class ApiVersioningOptionsBuilder : IConfigureOptions<ApiVersioningOptions>
    {
        /// <inheritdoc/>
        public void Configure(ApiVersioningOptions options)
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
        }
    }
}
