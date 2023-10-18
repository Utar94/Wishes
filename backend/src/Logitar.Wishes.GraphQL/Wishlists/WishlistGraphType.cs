using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class WishlistGraphType : AggregateGraphType<Wishlist>
{
  public WishlistGraphType() : base("Represents an user-defined wishlist in the system.")
  {
    Field(x => x.DisplayName)
      .Description("The display name of the wishlist.");
    Field(x => x.PictureUrl, nullable: true)
      .Description("The URL to the picture of the wishlist.");

    Field(x => x.ItemCount)
      .Description("The number of items in the wishlist.");
    Field(x => x.Items, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<ItemGraphType>>>))
      .Description("The items in the wishlist.");
  }
}
