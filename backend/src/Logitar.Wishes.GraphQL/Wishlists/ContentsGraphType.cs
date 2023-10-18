using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class ContentsGraphType : ObjectGraphType<Contents>
{
  public ContentsGraphType()
  {
    Name = nameof(Contents);
    Description = "Represents the page contents of a wishlist item.";

    Field(x => x.Text)
      .Description("The text of the contents.");
    Field(x => x.Type)
      .Description("The type of the contents.");
  }
}
