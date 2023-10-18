namespace Logitar.Wishes.Domain.Wishlists;

public interface IWishlistRepository
{
  Task<WishlistAggregate?> LoadAsync(WishlistId id, CancellationToken cancellationToken = default);
  Task<WishlistAggregate?> LoadAsync(WishlistId id, long? version = null, CancellationToken cancellationToken = default);
  Task SaveAsync(WishlistAggregate wishlist, CancellationToken cancellationToken = default);
}
