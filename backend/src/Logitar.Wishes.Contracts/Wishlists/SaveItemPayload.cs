namespace Logitar.Wishes.Contracts.Wishlists;

public record SaveItemPayload
{
  public string DisplayName { get; set; } = string.Empty;
  public string? Summary { get; set; }
  public string? PictureUrl { get; set; }

  public byte Rank { get; set; }
  public PricePayload? Price { get; set; }
  public ContentsPayload? Contents { get; set; }

  public List<string> Gallery { get; set; } = new();
  public List<string> Links { get; set; } = new();
}
