using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CartingService.WebApi;
using CartingService.WebApi.Api;
using CartingService.WebApi.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddTransient<IConfigureOptions<ApiVersioningOptions>, ApiVersioningOptionsBuilder>();
builder.Services.AddTransient<IConfigureOptions<ApiExplorerOptions>, ApiExplorerOptionsBuilder>();
builder.Services.AddApiVersioning()
    .AddApiExplorer()
    .AddMvc();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsBuilder>();
builder.Services.AddTransient<IConfigureOptions<SwaggerUIOptions>, SwaggerUiOptionsBuilder>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();