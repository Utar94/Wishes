using Logitar.Wishes.Application.Wishlists.Commands;
using Logitar.Wishes.Application.Wishlists.Queries;
using Logitar.Wishes.Contracts;
using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists;

internal class ItemService : IItemService
{
  private readonly IMediator _mediator;

  public ItemService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<Item?> ReadAsync(string wishlistId, string itemId, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadItemQuery(wishlistId, itemId), cancellationToken);
  }

  public async Task<AcceptedCommand> RemoveAsync(string wishlistId, string itemId, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new RemoveItemCommand(wishlistId, itemId), cancellationToken);
  }

  public async Task<AcceptedCommand> SaveAsync(string wishlistId, SaveItemPayload payload, string? itemId, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SaveItemCommand(wishlistId, payload, itemId), cancellationToken);
  }

  public async Task<SearchResults<Item>> SearchAsync(SearchItemsPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchItemsQuery(payload), cancellationToken);
  }

  public async Task<AcceptedCommand> UpdateAsync(string wishlistId, string itemId, UpdateItemPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new UpdateItemCommand(wishlistId, itemId, payload), cancellationToken);
  }
}
