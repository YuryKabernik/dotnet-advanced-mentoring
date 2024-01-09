using System.Net.Mime;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;
using CatalogService.WebApi.Requests.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Endpoints;

public static class ItemsEndpoints
{
    public static WebApplication UseItemsEndpoints(this WebApplication application)
    {
        var root = application.MapGroup("/api/items")
            .WithTags("Items")
            .WithOpenApi();

        root.MapGet("/category/{categoryId:guid}", GetAllItems)
            .Produces<List<Item>>()
            .WithSummary("List items (filtration by category id and pagination)")
            .WithDescription("GET /api/items/category/{category-id}?page={number}&perPage={number}");

        root.MapPost("/", AddItem)
            .Accepts<AddItemRequest>(MediaTypeNames.Application.Json)
            .Produces<Item>()
            .WithSummary("Add an item")
            .WithDescription("POST /api/items");

        root.MapPut("/{id:guid}", UpdateItem)
            .Accepts<UpdateItemRequest>(MediaTypeNames.Application.Json)
            .Produces<Item>()
            .WithSummary("Update an item")
            .WithDescription("PUT /api/items/{item-id}");

        root.MapDelete("/{id:guid}", DeleteItem)
            .WithSummary("Delete an item")
            .WithDescription("DELETE /api/items/{id}");

        return application;
    }

    public static async Task<IResult> GetAllItems(
        [FromRoute] Guid categoryId,
        [FromQuery] int page,
        [FromQuery] int perPage,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var query = new CategoryQuerySpec(categoryId);
        var category = await repository.GetAsync(query, cancellationToken);

        return Results.Ok(category.Items);
    }

    public static async Task<IResult> AddItem(
        [FromBody] AddItemRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result.Item);
    }

    public static async Task<IResult> UpdateItem(
        [FromRoute] Guid id,
        [FromBody] UpdateItemRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result.Item);
    }

    public static async Task<IResult> DeleteItem(
        [FromRoute] Guid id,
        IRepository<Item> repository,
        CancellationToken cancellationToken)
    {
        var query = new ItemQuerySpec(id);
        var item = await repository.GetAsync(query, cancellationToken);

        await repository.DeleteAsync(item, cancellationToken);

        return Results.Ok();
    }
}
