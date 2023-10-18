namespace Logitar.Wishes.Contracts.Wishlists;

public record ReplaceWishlistPayload
{
  public string DisplayName { get; set; } = string.Empty;
  public string? PictureUrl { get; set; }
}
