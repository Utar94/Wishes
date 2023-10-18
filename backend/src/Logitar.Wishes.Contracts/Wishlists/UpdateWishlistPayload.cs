namespace Logitar.Wishes.Contracts.Wishlists;

public record UpdateWishlistPayload
{
  public string? DisplayName { get; set; }
  public Modification<string>? PictureUrl { get; set; }
}
