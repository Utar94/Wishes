using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Web.Controllers;

[ApiController]
[Route("wishlists")]
public class WishlistController : ControllerBase // TODO(fpion): Authorization
{
  private readonly IWishlistService _wishlistService;

  public WishlistController(IWishlistService wishlistService)
  {
    _wishlistService = wishlistService;
  }

  [HttpPost]
  public async Task<ActionResult<AcceptedCommand>> CreateAsync([FromBody] CreateWishlistPayload payload, CancellationToken cancellationToken)
  {
    AcceptedCommand command = await _wishlistService.CreateAsync(payload, cancellationToken);
    Uri uri = new($"{Request.Scheme}://{Request.Host}/wishlists/{command.AggregateId}");

    return Created(uri, command);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<AcceptedCommand>> DeleteAsync(string id, CancellationToken cancellationToken)
  {
    return Ok(await _wishlistService.DeleteAsync(id, cancellationToken));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Wishlist>> ReadAsync(string id, CancellationToken cancellationToken)
  {
    Wishlist? wishlist = await _wishlistService.ReadAsync(id, cancellationToken);
    return wishlist == null ? NotFound() : Ok(wishlist);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<AcceptedCommand>> ReplaceAsync(string id, [FromBody] ReplaceWishlistPayload payload, long? version, CancellationToken cancellationToken)
  {
    return Ok(await _wishlistService.ReplaceAsync(id, payload, version, cancellationToken));
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<Wishlist>>> SearchAsync([FromQuery] SearchWishlistsQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _wishlistService.SearchAsync(query.ToPayload(), cancellationToken));
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<AcceptedCommand>> UpdateAsync(string id, [FromBody] UpdateWishlistPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _wishlistService.UpdateAsync(id, payload, cancellationToken));
  }
}
