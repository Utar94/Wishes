namespace Logitar.Wishes.Contracts.Wishlists;

public class Item : Metadata
{
  public string Id { get; set; } = string.Empty;

  public string DisplayName { get; set; } = string.Empty;
  public string? Summary { get; set; }
  public string? PictureUrl { get; set; }

  public byte Rank { get; set; }
  public byte RankCategory { get; set; }
  public Price? Price { get; set; }
  public Contents? Contents { get; set; }

  public List<string> Gallery { get; set; } = new();
  public List<string> Links { get; set; } = new();

  public Wishlist Wishlist { get; set; } = new();

  public override bool Equals(object? obj) => obj is Item item && item.Wishlist.Id == Wishlist.Id && item.Id == Id;
  public override int GetHashCode() => HashCode.Combine(GetType(), Wishlist.Id, Id);
  public override string ToString() => $"{DisplayName} | {GetType()} (WishlistId={Wishlist.Id}, ItemId={Id})";
}
