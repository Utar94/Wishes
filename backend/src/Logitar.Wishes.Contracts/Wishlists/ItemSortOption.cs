using Logitar.Wishes.Contracts.Search;

namespace Logitar.Wishes.Contracts.Wishlists;

public record ItemSortOption : SortOption
{
  public new ItemSort Field { get; set; }

  public ItemSortOption() : this(ItemSort.UpdatedOn, isDescending: true)
  {
  }
  public ItemSortOption(ItemSort field, bool isDescending = false) : base(field.ToString(), isDescending)
  {
  }
}
