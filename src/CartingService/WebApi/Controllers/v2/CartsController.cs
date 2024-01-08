using Asp.Versioning;
using CartingService.DataAccess.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.WebApi.Controllers.v2;

/// <summary>
/// Carting resource endpoints of version 2.
/// </summary>
[ApiVersion(2.0)]
[ApiController]
[Route("api/v{version:apiVersion}/carts/{cartId:alpha}")]
public class CartsController : ControllerBase
{
    /// <summary>
    /// Get cart info.
    /// </summary>
    /// <param name="cartId"></param>
    /// <returns>
    /// Returns a list of cart items instead of cart model.
    /// </returns>
    [HttpGet("/")]
    [ProducesResponseType<List<Item>>(StatusCodes.Status200OK)]
    public IResult Get(string cartId)
    {
        var items = new List<Item>();

        return Results.Ok(items);
    }
}