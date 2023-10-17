namespace Logitar.Wishes.Contracts.Wishlists;

public record ContentsPayload
{
  public string Text { get; set; } = string.Empty;
  public string Type { get; set; } = string.Empty;
}
