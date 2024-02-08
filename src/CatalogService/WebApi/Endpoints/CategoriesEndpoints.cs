﻿using System.Net.Mime;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;
using CatalogService.WebApi.Requests;
using CatalogService.WebApi.Requests.Mappings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.WebApi.Endpoints;

public static class CategoriesEndpoints
{
    public static WebApplication UseCategoryEndpoints(this WebApplication application)
    {
        var root = application.MapGroup("/api/categories")
            .WithTags("Categories")
            .WithOpenApi();

        root.MapGet("/", GetAllCategories)
            .Produces<List<Category>>()
            .WithSummary("List of categories")
            .WithDescription("GET /api/categories");

        root.MapPost("/", AddCategory)
            .Accepts<AddCategoryRequest>(MediaTypeNames.Application.Json)
            .Produces<Category>()
            .WithSummary("Add a new category")
            .WithDescription("POST /api/categories");

        root.MapPut("/{id:guid}", UpdateCategory)
            .Accepts<Category>(MediaTypeNames.Application.Json)
            .Produces<Category>()
            .WithSummary("Update a Category by their Id")
            .WithDescription("PUT /api/categories/{id}");

        root.MapDelete("/{id:guid}", DeleteCategory)
            .Produces<List<Category>>()
            .WithSummary("Delete a Category by their Id")
            .WithDescription("DELETE /api/categories/{id}");

        return application;
    }

    public static async Task<IResult> GetAllCategories(
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var spec = new CategoryQuerySpec();
        var result = await repository.ListAsync(spec, cancellationToken);
    
        return Results.Ok(result);
    }

    public static async Task<IResult> AddCategory(
        [FromBody] AddCategoryRequest addRequest,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = addRequest.ToCommand();
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result.Category);
    }

    public static async Task<IResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest updateRequest,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = updateRequest.ToCommand(id);
        var result = await mediator.Send(command, cancellationToken);

        return Results.Ok(result.Category);
    }

    public static async Task<IResult> DeleteCategory(
        [FromRoute] Guid id,
        IRepository<Category> repository,
        CancellationToken cancellationToken)
    {
        var spec = new CategoryQuerySpec(id);
        var category = await repository.GetAsync(spec, cancellationToken);

        await repository.DeleteAsync(category, cancellationToken);

        return Results.Ok();
    }
}