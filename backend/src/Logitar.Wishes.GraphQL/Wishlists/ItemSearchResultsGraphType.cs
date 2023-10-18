using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class ItemSearchResultsGraphType : SearchResultsGraphType<ItemGraphType, Item>
{
  public ItemSearchResultsGraphType() : base("ItemSearchResults", "Represents the results of a wishlist item search.")
  {
  }
}
