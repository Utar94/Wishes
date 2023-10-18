namespace Logitar.Wishes.Contracts.Wishlists;

public record UrlAction
{
  public string Url { get; set; }
  public CollectionAction Action { get; set; }

  public UrlAction() : this(string.Empty)
  {
  }
  public UrlAction(string url, CollectionAction action = CollectionAction.Add)
  {
    Url = url;
    Action = action;
  }
}
