using GraphQL.Types;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.GraphQL.Wishlists;

internal class PriceGraphType : ObjectGraphType<Price>
{
  public PriceGraphType()
  {
    Name = nameof(Price);
    Description = "Represents the price of a wishlist item.";

    Field(x => x.Average)
      .Description("The average price of the item.");
    Field(x => x.Minimum)
      .Description("The minimum price of the item.");
    Field(x => x.Maximum)
      .Description("The maximum price of the item.");
    Field(x => x.Category)
      .Description("The price category of the item.");
  }
}
