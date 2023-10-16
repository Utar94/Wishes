namespace Logitar.Wishes.Contracts.Constants;

public static class Schemes
{
  public const string ApiKey = nameof(ApiKey);
  public const string Basic = nameof(Basic);

  public static string[] All => new[] { ApiKey, Basic };
}
