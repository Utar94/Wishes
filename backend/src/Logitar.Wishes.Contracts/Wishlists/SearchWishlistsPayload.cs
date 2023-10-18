using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public record SearchWishlistsPayload : SearchPayload
{
  public new List<WishlistSortOption> Sort { get; set; } = new();
}
