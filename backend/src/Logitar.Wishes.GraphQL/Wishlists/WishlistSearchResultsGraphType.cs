using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;
using Logitar.Wishes.GraphQL.Wishlists;

namespace Logitar.Portal.GraphQL.Wishlists;

internal class WishlistSearchResultsGraphType : SearchResultsGraphType<WishlistGraphType, Wishlist>
{
  public WishlistSearchResultsGraphType() : base("WishlistSearchResults", "Represents the results of a wishlist search.")
  {
  }
}
