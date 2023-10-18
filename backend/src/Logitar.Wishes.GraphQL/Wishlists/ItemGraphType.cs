using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class ItemGraphType : MetadataGraphType<Item>
{
  public ItemGraphType()
  {
    Name = nameof(Item);
    Description = "Represents an item in a wishlist.";

    Field(x => x.Id)
      .Description("The unique identifier of the wishlist item.");

    Field(x => x.DisplayName)
      .Description("The display name of the wishlist item.");
    Field(x => x.Summary, nullable: true)
      .Description("The summary of the wishlist item.");
    Field(x => x.PictureUrl, nullable: true)
      .Description("The URL to the main picture of the wishlist item.");

    Field(x => x.Rank)
      .Description("The rank of the wishlist item. The lower, the more important the item is within the wishlist.");
    Field(x => x.RankCategory)
      .Description("The category of the wishlist item's rank. The lower, the less important the item is within the wishlist.");
    Field(x => x.Price, type: typeof(PriceGraphType))
      .Description("The price detail of the wishlist item.");
    Field(x => x.Contents, type: typeof(ContentsGraphType))
      .Description("The page contents of the wishlist item.");

    Field(x => x.Gallery, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>))
      .Description("The URL to the gallery pictures of the wishlist item.");
    Field(x => x.Links, type: typeof(NonNullGraphType<ListGraphType<NonNullGraphType<StringGraphType>>>))
      .Description("External link URLs of the wishlist item.");

    Field(x => x.Wishlist, type: typeof(NonNullGraphType<WishlistGraphType>))
      .Description("The wishlist in which the item belongs to.");
  }
}
