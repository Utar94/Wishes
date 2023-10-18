namespace Logitar.Wishes.Web.Models.Version;

public record ApplicationVersion
{
  public string Version { get; }

  public ApplicationVersion(System.Version version)
  {
    Version = version.ToString();
  }
}
