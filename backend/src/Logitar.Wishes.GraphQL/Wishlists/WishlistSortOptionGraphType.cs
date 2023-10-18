using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class WishlistSortOptionGraphType : SortOptionInputGraphType<WishlistSortOption>
{
  public WishlistSortOptionGraphType() : base()
  {
    Field(x => x.Field, type: typeof(NonNullGraphType<WishlistSortGraphType>))
      .Description("The field on which to apply the sort.");
  }
}
