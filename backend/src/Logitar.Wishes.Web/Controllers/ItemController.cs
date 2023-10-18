using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Wishes.Web.Controllers;

[ApiController]
[Route("wishlists/{wishlistId}/items")]
public class ItemController : ControllerBase
{
  private readonly IItemService _itemService;

  public ItemController(IItemService itemService)
  {
    _itemService = itemService;
  }

  [Authorize(Policy = Policies.CanWriteWishlists)]
  [HttpPost]
  public async Task<ActionResult<AcceptedCommand>> CreateAsync(string wishlistId, [FromBody] SaveItemPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _itemService.SaveAsync(wishlistId, payload, itemId: null, cancellationToken));
  }

  [Authorize(Policy = Policies.CanWriteWishlists)]
  [HttpDelete("{itemId}")]
  public async Task<ActionResult<AcceptedCommand>> DeleteAsync(string wishlistId, string itemId, CancellationToken cancellationToken)
  {
    return Ok(await _itemService.RemoveAsync(wishlistId, itemId, cancellationToken));
  }

  [Authorize(Policy = Policies.CanReadWishlists)]
  [HttpGet("{itemId}")]
  public async Task<ActionResult<Item>> ReadAsync(string wishlistId, string itemId, CancellationToken cancellationToken)
  {
    Item? item = await _itemService.ReadAsync(wishlistId, itemId, cancellationToken);
    return item == null ? NotFound() : Ok(item);
  }

  [Authorize(Policy = Policies.CanWriteWishlists)]
  [HttpPut("{itemId}")]
  public async Task<ActionResult<AcceptedCommand>> ReplaceAsync(string wishlistId, string itemId, [FromBody] SaveItemPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _itemService.SaveAsync(wishlistId, payload, itemId, cancellationToken));
  }

  [Authorize(Policy = Policies.CanReadWishlists)]
  [HttpGet]
  public async Task<ActionResult<SearchResults<Item>>> SearchAsync(string wishlistId, [FromQuery] SearchItemsQuery query, CancellationToken cancellationToken)
  {
    return Ok(await _itemService.SearchAsync(query.ToPayload(wishlistId), cancellationToken));
  }

  [Authorize(Policy = Policies.CanWriteWishlists)]
  [HttpPatch("{itemId}")]
  public async Task<ActionResult<AcceptedCommand>> UpdateAsync(string wishlistId, string itemId, [FromBody] UpdateItemPayload payload, CancellationToken cancellationToken)
  {
    return Ok(await _itemService.UpdateAsync(wishlistId, itemId, payload, cancellationToken));
  }
}
