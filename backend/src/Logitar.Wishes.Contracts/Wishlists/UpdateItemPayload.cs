namespace Logitar.Wishes.Contracts.Wishlists;

public record UpdateItemPayload
{
  public string? DisplayName { get; set; }
  public Modification<string>? Summary { get; set; }
  public Modification<string>? PictureUrl { get; set; }

  public byte? Rank { get; set; }
  public Modification<PricePayload>? Price { get; set; }
  public Modification<ContentsPayload>? Contents { get; set; }

  public List<UrlAction> Gallery { get; set; } = new();
  public List<UrlAction> Links { get; set; } = new();
}
