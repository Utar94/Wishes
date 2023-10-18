using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Wishlists;
using MediatR;

namespace Logitar.Wishes.Application.Wishlists.Queries;

internal class ReadItemQueryHandler : IRequestHandler<ReadItemQuery, Item?>
{
  private readonly IItemQuerier _itemQuerier;

  public ReadItemQueryHandler(IItemQuerier itemQuerier)
  {
    _itemQuerier = itemQuerier;
  }

  public async Task<Item?> Handle(ReadItemQuery query, CancellationToken cancellationToken)
  {
    WishlistId wishlistId = WishlistId.Parse(query.WishlistId, nameof(query.WishlistId));
    ItemId itemId = ItemId.Parse(query.ItemId, nameof(query.ItemId));

    return await _itemQuerier.ReadAsync(wishlistId, itemId, cancellationToken);
  }
}
