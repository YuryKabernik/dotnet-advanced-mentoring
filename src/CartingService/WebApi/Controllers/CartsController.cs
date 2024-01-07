using System.Net.Mime;
using Asp.Versioning;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;
using CartingService.WebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers;

/// <summary>
/// Carting resource endpoints.
/// </summary>
[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/carts/{cartId:alpha}")]
public class CartsController : ControllerBase
{
    /// <summary>
    ///     Get cart info.
    /// </summary>
    /// <param name="cartId">
    ///     Cart unique Key (string)
    /// </param>
    /// <returns>
    ///     Returns a cart model (cart key + list of cart items).
    /// </returns>
    [HttpGet("/")]
    [ProducesResponseType<Cart>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IResult Get(string cartId)
    {
        var cart = new Cart() { PrimaryId = default, RawId = default };
        
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
    public IResult Add(string cartId, CartNewItem newItem)
    {
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
    [HttpDelete("/{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult Delete(string cartId, int itemId)
    {
        return Results.Ok();
    }
}