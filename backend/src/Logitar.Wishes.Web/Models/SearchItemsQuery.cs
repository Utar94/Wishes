using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.Web.Models;

public record SearchItemsQuery : SearchQuery
{
  public SearchItemsPayload ToPayload(string wishlistId)
  {
    SearchItemsPayload payload = new()
    {
      WishlistId = wishlistId
    };

    ApplyQuery(payload);

    List<SortOption> sort = ((SearchPayload)payload).Sort;
    payload.Sort = new List<ItemSortOption>(sort.Capacity);
    foreach (SortOption option in sort)
    {
      if (Enum.TryParse(option.Field, out ItemSort field))
      {
        payload.Sort.Add(new ItemSortOption(field, option.IsDescending));
      }
    }

    return payload;
  }
}
