using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public record WishlistSortOption : SortOption
{
  public new WishlistSort Field { get; set; }

  public WishlistSortOption() : this(WishlistSort.UpdatedOn, isDescending: true)
  {
  }
  public WishlistSortOption(WishlistSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
    Field = field;
  }
}
