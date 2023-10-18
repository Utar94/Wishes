using GraphQL;
using GraphQL.Types;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Extensions;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal static class ItemQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<ItemGraphType>("item")
      .AuthorizeWithPolicy(Policies.CanReadWishlists)
      .Description("Retrieves a single wishlist item.")
      .Arguments(
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "wishlistId", Description = "The unique identifier of the wishlist." },
        new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "itemId", Description = "The unique identifier of the item." }
      )
      .ResolveAsync(async context => await context.GetRequiredService<IItemService, object?>().ReadAsync(
        context.GetArgument<string>("wishlistId"),
        context.GetArgument<string>("itemId"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<ItemSearchResultsGraphType>>("items")
      .AuthorizeWithPolicy(Policies.CanReadWishlists)
      .Description("Searches a list of wishtlist items.")
      .Arguments(new QueryArgument<NonNullGraphType<SearchItemsPayloadGraphType>>() { Name = "payload", Description = "The parameters to apply to the search." })
      .ResolveAsync(async context => await context.GetRequiredService<IItemService, object?>().SearchAsync(
        context.GetArgument<SearchItemsPayload>("payload"),
        context.CancellationToken
      ));
  }
}
