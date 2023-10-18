using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Search;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class ItemSortOptionGraphType : SortOptionInputGraphType<ItemSortOption>
{
  public ItemSortOptionGraphType() : base()
  {
    Field(x => x.Field, type: typeof(NonNullGraphType<ItemSortGraphType>))
      .Description("The field on which to apply the sort.");
  }
}
