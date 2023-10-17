using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.Domain.Wishlists;

namespace Logitar.Wishes.Application.Wishlists;

public interface IItemQuerier
{
  Task<Item?> ReadAsync(WishlistId wishlistId, ItemId itemId, CancellationToken cancellationToken = default);
}
