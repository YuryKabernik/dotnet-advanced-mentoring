using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.WebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Endpoints;

public static class CategoriesEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication application)
    {
        var root = application.MapGroup("/api/categories")
            .WithTags("categories", "category")
            .WithOpenApi();

        root.MapGet("/", GetAllCategories)
            .Produces<List<Category>>()
            .WithSummary("List of categories")
            .WithDescription("GET /api/categories");

        root.MapPost("/", AddCategory)
            .Accepts<AddCategoryRequest>("application/json")
            .Produces<List<Category>>()
            .WithSummary("Add a new category")
            .WithDescription("POST /api/categories");

        root.MapPut("/{id:guid}", UpdateCategory)
            .Accepts<UpdateCategoryRequest>("application/json")
            .Produces<List<Category>>()
            .WithSummary("Update a Category by their Id")
            .WithDescription("PUT /api/categories");

        root.MapDelete("/{id:guid}", DeleteCategory)
            .Produces<List<Category>>()
            .WithSummary("Delete a Category by their Id")
            .WithDescription("DELETE /api/categories");

        return application;
    }

    private static Task<List<Category>> GetAllCategories(
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var spec = new CategorySpec();

        return repository.ListAsync(spec, cancellationToken);
    }

    private static Task AddCategory(
        [FromBody] AddCategoryRequest newCategoryRequest,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.Empty,
            Name = "Name"
        };
        return repository.AddAsync(category, cancellationToken);
    }

    private static Task UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest updateCategoryRequest,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.Empty,
            Name = "New Name"
        };
        return repository.UpdateAsync(category, cancellationToken);
    }

    private static Task DeleteCategory(
        [FromRoute] Guid id,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.Empty,
            Name = "Name"
        };
        return repository.DeleteAsync(category, cancellationToken);
    }
}
