using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public interface IWishlistService
{
  Task<AcceptedCommand> CreateAsync(CreateWishlistPayload payload, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> DeleteAsync(string id, CancellationToken cancellationToken = default);
  Task<Wishlist?> ReadAsync(string id, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> ReplaceAsync(string id, ReplaceWishlistPayload payload, long? version = null, CancellationToken cancellationToken = default);
  Task<SearchResults<Wishlist>> SearchAsync(SearchWishlistsPayload payload, CancellationToken cancellationToken = default);
  Task<AcceptedCommand> UpdateAsync(string id, UpdateWishlistPayload payload, CancellationToken cancellationToken = default);
}
