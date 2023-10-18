namespace Logitar.Wishes.Web.Models.Share;

public record SharedLink
{
  public string Href { get; set; }

  public SharedLink() : this(string.Empty)
  {
  }
  public SharedLink(Uri uri) : this(uri.ToString())
  {
  }
  public SharedLink(string href)
  {
    Href = href;
  }
}
