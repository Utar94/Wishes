using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;

namespace Logitar.Wishes.Web.Extensions;

internal static class HttpContextExtensions
{
  private const string ApiKeyKey = "ApiKey";
  private const string UserKey = "User";

  public static ApiKey? GetApiKey(this HttpContext context) => context.GetItem<ApiKey>(ApiKeyKey);
  public static User? GetUser(this HttpContext context) => context.GetItem<User>(UserKey);
  private static T? GetItem<T>(this HttpContext context, object key)
  {
    return context.Items.TryGetValue(key, out object? value) ? (T?)value : default;
  }

  public static void SetApiKey(this HttpContext context, ApiKey? apiKey) => context.SetItem(ApiKeyKey, apiKey);
  public static void SetUser(this HttpContext context, User? user) => context.SetItem(UserKey, user);
  private static void SetItem<T>(this HttpContext context, object key, T? value)
  {
    if (value == null)
    {
      context.Items.Remove(key);
    }
    else
    {
      context.Items[key] = value;
    }
  }
}
