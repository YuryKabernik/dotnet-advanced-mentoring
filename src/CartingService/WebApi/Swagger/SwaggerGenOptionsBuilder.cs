using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CartingService.WebApi.Swagger;

/// <summary>
/// Configuration options builder for the Swagger Gen component.
/// </summary>
public class SwaggerGenOptionsBuilder(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SwaggerOperationFilter>();

        // add a swagger document for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            OpenApiInfo info = CreateInfoForApiVersion(description);

            options.SwaggerDoc(description.GroupName, info);
        }

        IncludeXmlComments(options);
    }

    /// <summary>
    /// Inject human-friendly descriptions for Operations, Parameters and Schemas based on XML Comment files
    /// </summary>
    /// <param name="options"></param>
    private static void IncludeXmlComments(SwaggerGenOptions options)
    {
        var xmlFilename = $"{typeof(SwaggerGenOptionsBuilder).Assembly.GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }

    /// <summary>
    /// Add a swagger document for each discovered API version.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var text = new StringBuilder("An example application with OpenAPI, Swashbuckle, and API versioning.");
        var info = new OpenApiInfo()
        {
            Title = "Carting API",
            Version = $"API Version: {description.ApiVersion}",
        };

        // note: you might choose to skip or document deprecated API versions differently
        if (description.IsDeprecated)
        {
            text.Append(" This API version has been deprecated.");
        }

        if (description.SunsetPolicy is SunsetPolicy policy)
        {
            if (policy.Date is DateTimeOffset when)
            {
                text.Append(" The API will be sunset on ")
                    .Append(when.Date.ToShortDateString())
                    .Append('.');
            }

            if (policy.HasLinks)
            {
                text.AppendLine();

                for (var i = 0; i < policy.Links.Count; i++)
                {
                    var link = policy.Links[i];

                    if (link.Type == "text/html")
                    {
                        text.AppendLine();

                        if (link.Title.HasValue)
                        {
                            text.Append(link.Title.Value).Append(": ");
                        }

                        text.Append(link.LinkTarget.OriginalString);
                    }
                }
            }
        }

        info.Description = text.ToString();

        return info;
    }
}