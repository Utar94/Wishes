using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class SearchItemsPayloadGraphType : SearchPayloadInputGraphType<SearchItemsPayload>
{
  public SearchItemsPayloadGraphType() : base()
  {
    Field(x => x.WishlistId)
      .Description("The unique identifier of the wishlist to search within.");

    Field(x => x.Sort, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<ItemSortOptionGraphType>>>))
      .DefaultValue(new List<ItemSortOption>())
      .Description("The sort parameters of the search.");
  }
}
