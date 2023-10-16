using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public interface IWishlistService
{
  Task<Wishlist?> ReadAsync(string id, CancellationToken cancellationToken = default);
  Task<SearchResults<Wishlist>> SearchAsync(SearchWishlistsPayload payload, CancellationToken cancellationToken = default);
}
