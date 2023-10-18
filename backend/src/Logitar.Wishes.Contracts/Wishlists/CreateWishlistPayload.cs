namespace Logitar.Wishes.Contracts.Wishlists;

public record CreateWishlistPayload
{
  public string? Id { get; set; }

  public string DisplayName { get; set; } = string.Empty;
  public string? PictureUrl { get; set; }
}
