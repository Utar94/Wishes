namespace Logitar.Wishes.Web.Settings;

public record Roles
{
  public string ReadWishlists { get; set; } = "read_wishlists";
  public string ShareWishlists { get; set; } = "share_wishlists";
  public string WriteWishlists { get; set; } = "write_wishlists";
}
