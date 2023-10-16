using Logitar.Wishes.Contracts.Search;
using Logitar.Wishes.Contracts.Wishlists;

namespace Logitar.Wishes.Web.Models;

public record SearchWishlistsQuery : SearchQuery
{
  public new SearchWishlistsPayload ToPayload()
  {
    SearchWishlistsPayload payload = new();

    ApplyQuery(payload);

    List<SortOption> sort = ((SearchPayload)payload).Sort;
    payload.Sort = new List<WishlistSortOption>(sort.Capacity);
    foreach (SortOption option in sort)
    {
      if (Enum.TryParse(option.Field, out WishlistSort field))
      {
        payload.Sort.Add(new WishlistSortOption(field, option.IsDescending));
      }
    }

    return payload;
  }
}
