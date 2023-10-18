namespace Logitar.Wishes.Infrastructure.Security;

internal record XApiKey
{
  private const string Prefix = "PT";
  private const char Separator = '.';

  public string Id { get; }
  public string Secret { get; }

  private XApiKey(string id, string secret)
  {
    Id = id;
    Secret = secret;
  }

  public static XApiKey Decode(string s)
  {
    string[] values = s.Split(Separator);
    if (values.First() != Prefix || values.Length != 3)
    {
      throw new ArgumentException($"The value '{s}' is not a valid X-API-Key.", nameof(s));
    }

    return new XApiKey(values[1], values[2]);
  }
  public static bool TryDecode(string s, out XApiKey? xApiKey)
  {
    try
    {
      xApiKey = Decode(s);

      return true;
    }
    catch (Exception)
    {
      xApiKey = null;

      return false;
    }
  }

  public string Encode() => string.Join(Separator, Prefix, Id, Secret);
}
