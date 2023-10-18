using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class WishlistSearchResultsGraphType : SearchResultsGraphType<WishlistGraphType, Wishlist>
{
  public WishlistSearchResultsGraphType() : base("WishlistSearchResults", "Represents the results of a wishlist search.")
  {
  }
}
