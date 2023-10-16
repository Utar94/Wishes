using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Portal.GraphQL.Wishlists;

internal class WishlistSortGraphType : EnumerationGraphType<WishlistSort>
{
  public WishlistSortGraphType()
  {
    Name = nameof(WishlistSort);
    Description = "Represents the available wishlist fields for sorting.";

    Add(WishlistSort.DisplayName, "The wishlists will be sorted by their display name.");
    Add(WishlistSort.ItemCount, "The wishlists will be sorted by their item count.");
    Add(WishlistSort.UpdatedOn, "The wishlists will be sorted by their latest update date and time.");
  }

  private void Add(WishlistSort value, string description) => Add(value.ToString(), value, description);
}
