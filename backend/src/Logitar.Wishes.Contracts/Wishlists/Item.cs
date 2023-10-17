namespace Logitar.Wishes.Contracts.Wishlists;

public class Item
{
  public string Id { get; set; } = string.Empty;

  public string DisplayName { get; set; } = string.Empty;
  public string? Summary { get; set; }
  public string? PictureUrl { get; set; }

  public byte Rank { get; set; }
  public Price? Price { get; set; }
  public Contents? Contents { get; set; }

  public List<string> Gallery { get; set; } = new();
  public List<string> Links { get; set; } = new();

  public override string ToString() => $"{DisplayName} | {base.ToString()}";
}
