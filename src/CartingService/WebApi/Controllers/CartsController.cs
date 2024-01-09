using System.Net.Mime;
using System.Threading;
using Asp.Versioning;
using CartingService.BusinessLogic.Commands.Add;
using CartingService.BusinessLogic.Commands.Remove;
using CartingService.BusinessLogic.Queries.Items;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;
using CartingService.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers;

/// <summary>
/// Carting resource endpoints.
/// </summary>
[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/carts/{cartId:alpha}")]
public class CartsController(IMediator mediator) : ControllerBase
{

    /// <summary>
    ///     Get cart info.
    /// </summary>
    /// <param name="cartId">
    ///     Cart unique Key (string)
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     Returns a cart model (cart key + list of cart items).
    /// </returns>
    [HttpGet("/")]
    [ProducesResponseType<Cart>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IResult> GetAsync(string cartId, CancellationToken cancellationToken)
    {
        var query = new ItemsQueryRequest(cartId);
        var cart = await mediator.Send(query, cancellationToken);

        return Results.Ok(cart);
    }

    /// <summary>
    ///     Add item to cart.
    /// </summary>
    /// <param name="cartId">
    ///     Cart unique Key (string)
    /// </param>
    /// <param name="newItem">
    ///     Cart item model
    /// </param>
    /// <returns>
    ///     Returns 200 if item was added to the cart
    ///     If there was no cart for specified key – creates it.
    ///     Otherwise returns a corresponding HTTP code.
    /// </returns>
    [HttpPost("/")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Item>(StatusCodes.Status200OK)]
    [ProducesResponseType<Item>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> AddAsync(string cartId, CartNewItem newItem, CancellationToken cancellationToken)
    {
        var item = new NewItem(newItem.Id, newItem.Quantity);
        var command = new AddCommandRequest(cartId, item);

        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    }

    /// <summary>
    ///     Delete item from cart.
    /// </summary>
    /// <param name="cartId">
    ///     cart unique key (string)
    /// </param>
    /// <param name="itemId">
    ///     Item id (int).
    /// </param>
    /// <returns>
    ///     Returns 200 if item was deleted, otherwise returns corresponding HTTP code.
    /// </returns>
    [HttpDelete("/{itemId:alpha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteAsync(string cartId, string itemId, CancellationToken cancellationToken)
    {
        var command = new RemoveCommandRequest(cartId, itemId);
        await mediator.Send(command, cancellationToken);

        return Results.Ok();
    }
}