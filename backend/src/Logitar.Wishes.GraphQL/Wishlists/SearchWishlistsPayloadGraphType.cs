using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Portal.GraphQL.Wishlists;

internal class SearchWishlistsPayloadGraphType : SearchPayloadInputGraphType<SearchWishlistsPayload>
{
  public SearchWishlistsPayloadGraphType() : base()
  {
    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<WishlistSortOptionGraphType>>>))
      .DefaultValue(new List<WishlistSortOption>())
      .Description("The sort parameters of the search.");
  }
}
