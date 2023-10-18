using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.Application.Wishlists;

public interface IWishlistQuerier
{
  Task<Wishlist?> ReadAsync(string id, CancellationToken cancellationToken = default);
  Task<SearchResults<Wishlist>> SearchAsync(SearchWishlistsPayload payload, CancellationToken cancellationToken = default);
}
