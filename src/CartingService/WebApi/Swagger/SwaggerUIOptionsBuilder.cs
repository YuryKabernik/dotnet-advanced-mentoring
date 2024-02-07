using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CartingService.WebApi.Swagger;

/// <summary>
/// Configuration options builder for the Swagger UI component.
/// </summary>
public class SwaggerUiOptionsBuilder(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerUIOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerUIOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            BuildSwaggerEndpoint(options, description);
        }
    }

    /// <summary>
    /// Build a swagger endpoint for a discovered API version
    /// </summary>
    /// <param name="options"></param>
    /// <param name="description"></param>
    private static void BuildSwaggerEndpoint(SwaggerUIOptions options, ApiVersionDescription description)
    {
        var url = $"/swagger/{description.GroupName}/swagger.json";
        var name = $"Web API Version: {description.GroupName}";

        options.SwaggerEndpoint(url, name);
    }
}