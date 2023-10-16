using Logitar.Portal.Client;

namespace Logitar.Wishes.Web.Settings;

public record IdentitySettings : PortalSettings
{
  public string Realm { get; set; } = "wishes";
  public Roles Roles { get; set; } = new();
}

public record Roles
{
  public string ReadWishlists { get; set; } = "read_wishlists";
  public string WriteWishlists { get; set; } = "write_wishlists";
}
