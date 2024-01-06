using Application.Items.AddCommand;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;
using CatalogService.WebApi.Requests.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Endpoints;

public static class ItemsEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication application)
    {
        var root = application.MapGroup("/api/items")
            .WithTags("items", "item")
            .WithOpenApi();

        root.MapGet("/category/{categoryId:guid}", GetAllItems)
            .Produces<List<Item>>()
            .WithSummary("List items (filtration by category id and pagination)")
            .WithDescription("GET /api/items/category/{category-id}?page={number}&perPage={number}");

        root.MapPost("/", AddItem)
            .Accepts<AddItemRequest>("application/json")
            .Produces<Item>()
            .WithSummary("Add an item")
            .WithDescription("POST /api/items");

        root.MapPut("/{id:guid}", UpdateItem)
            .Accepts<UpdateItemRequest>("application/json")
            .Produces<Item>()
            .WithSummary("Update an item")
            .WithDescription("PUT /api/items/{item-id}");

        root.MapDelete("/{id:guid}", DeleteItem)
            .WithSummary("Delete an item")
            .WithDescription("DELETE /api/items/{id}");

        return application;
    }

    private static async Task<List<Item>> GetAllItems(
        [FromRoute] Guid categoryId,
        [FromQuery] int page,
        [FromQuery] int perPage,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var query = new CategoryQuerySpec(categoryId);
        var category = await repository.GetAsync(query, cancellationToken);

        return category.Items.ToList();
    }

    private static async Task<Item> AddItem(
        [FromBody] AddItemRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await mediator.Send(command, cancellationToken);

        return result.Item;
    }

    private static async Task UpdateItem(
        [FromRoute] Guid id,
        [FromBody] UpdateItemRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await mediator.Send(command, cancellationToken);
    }

    private static async Task DeleteItem(
        [FromRoute] Guid id,
        IRepository<Item> repository,
        CancellationToken cancellationToken)
    {
        var query = new ItemQuerySpec(id);
        var item = await repository.GetAsync(query, cancellationToken);

        await repository.DeleteAsync(item, cancellationToken);
    }
}