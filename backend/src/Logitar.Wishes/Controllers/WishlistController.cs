using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Controllers;

[ApiController]
[Route("wishlists")]
public class WishlistController : ControllerBase // TODO(fpion): Authorization
{
  private readonly IWishlistService _wishlistService;

  public WishlistController(IWishlistService wishlistService)
  {
    _wishlistService = wishlistService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Wishlist>> ReadAsync(string id, CancellationToken cancellationToken)
  {
    Wishlist? wishlist = await _wishlistService.ReadAsync(id, cancellationToken);
    return wishlist == null ? NotFound() : Ok(wishlist);
  }

  [HttpGet]
  public async Task<ActionResult<SearchResults<Wishlist>>> SearchAsync([FromQuery] SearchWishlistsQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _wishlistService.SearchAsync(query.ToPayload(), cancellationToken));
  }
}
