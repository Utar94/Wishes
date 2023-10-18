using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class ItemSortGraphType : EnumerationGraphType<ItemSort>
{
  public ItemSortGraphType()
  {
    Name = nameof(ItemSort);
    Description = "Represents the available wishlist item fields for sorting.";

    Add(ItemSort.AveragePrice, "The items will be sorted by their average price.");
    Add(ItemSort.DisplayName, "The items will be sorted by their display name.");
    Add(ItemSort.Rank, "The items will be sorted by their rank.");
    Add(ItemSort.UpdatedOn, "The items will be sorted by their latest update date and time.");
  }

  private void Add(ItemSort value, string description) => Add(value.ToString(), value, description);
}
