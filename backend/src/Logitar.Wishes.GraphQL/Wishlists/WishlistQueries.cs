using GraphQL;
using GraphQL.Types;
using Logitar.Portal.GraphQL.Wishlists;
using Logitar.Wishes.Contracts.Constants;
using Logitar.Wishes.Contracts.Wishlists;
using Logitar.Wishes.GraphQL.Extensions;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal static class WishlistQueries
{
  public static void Register(RootQuery root)
  {
    root.Field<WishlistGraphType>("wishlist")
      .AuthorizeWithPolicy(Policies.CanReadWishlists)
      .Description("Retrieves a single wishlist.")
      .Arguments(new QueryArgument<StringGraphType>() { Name = "id", Description = "The unique identifier of the wishlist." })
      .ResolveAsync(async context => await context.GetRequiredService<IWishlistService, object?>().ReadAsync(
        context.GetArgument<string>("id"),
        context.CancellationToken
      ));

    root.Field<NonNullGraphType<WishlistSearchResultsGraphType>>("wishlists")
      .AuthorizeWithPolicy(Policies.CanReadWishlists)
      .Description("Searches a list of wishlists.")
      .Arguments(new QueryArgument<NonNullGraphType<SearchWishlistsPayloadGraphType>>() { Name = "payload", Description = "The parameters to apply to the search." })
      .ResolveAsync(async context => await context.GetRequiredService<IWishlistService, object?>().SearchAsync(
        context.GetArgument<SearchWishlistsPayload>("payload"),
        context.CancellationToken
      ));
  }
}
