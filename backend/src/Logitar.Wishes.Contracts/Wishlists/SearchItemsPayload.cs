using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public record SearchItemsPayload : SearchPayload
{
  public string WishlistId { get; set; } = string.Empty;

  public new List<ItemSortOption> Sort { get; set; } = new();
}
