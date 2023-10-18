namespace Logitar.Wishes.Contracts.Wishlists;

public class Wishlist : Aggregate
{
  public string DisplayName { get; set; } = string.Empty;
  public string? PictureUrl { get; set; }

  public int ItemCount { get; set; }
  public List<Item> Items { get; set; } = new();

  public override string ToString() => $"{DisplayName} | {base.ToString()}";
}
