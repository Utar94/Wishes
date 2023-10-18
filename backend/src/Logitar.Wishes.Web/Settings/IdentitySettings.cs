using Logitar.Portal.Client;

namespace Logitar.Wishes.Web.Settings;

public record IdentitySettings : PortalSettings
{
  public string Realm { get; set; } = "wishes";
  public Roles Roles { get; set; } = new();
}
