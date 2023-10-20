using Logitar.EventSourcing;
using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Contracts.Actors;
using Logitar.Wishes.Infrastructure.Security;
using Microsoft.Extensions.Caching.Memory;

namespace Logitar.Wishes.Infrastructure.Caching;

internal class CacheService : ICacheService
{
  private readonly IMemoryCache _cache;

  public CacheService(IMemoryCache cache)
  {
    _cache = cache;
  }

  public Actor? GetActor(ActorId id) => GetItem<Actor>(GetActorKey(id));
  public void SetActor(Actor actor) => SetItem(GetActorKey(actor.Id), actor, TimeSpan.FromMinutes(15));
  private static string GetActorKey(string id) => GetActorKey(new ActorId(id));
  private static string GetActorKey(ActorId id) => $"Actor:Id:{id}";

  public ApiKey? GetApiKey(string xApiKey)
  {
    XApiKey instance = XApiKey.Decode(xApiKey);
    ProtectedCache<ApiKey>? cached = GetItem<ProtectedCache<ApiKey>>(GetApiKeyKey(instance.Id));

    return cached?.GetValue(instance.Secret);
  }
  public void SetApiKey(string xApiKey, ApiKey apiKey)
  {
    XApiKey instance = XApiKey.Decode(xApiKey);
    ProtectedCache<ApiKey> cached = new(apiKey, instance.Secret);

    SetItem(GetApiKeyKey(instance.Id), cached, TimeSpan.FromMinutes(5));
  }
  private static string GetApiKeyKey(string id) => $"ApiKey:Id:{id}";

  public User? GetUser(string username, string password)
  {
    ProtectedCache<User>? cached = GetItem<ProtectedCache<User>>(GetUserKey(username));

    return cached?.GetValue(password);
  }
  public void SetUser(string username, string password, User user)
  {
    ProtectedCache<User> cached = new(user, password);

    SetItem(GetUserKey(username), cached, TimeSpan.FromMinutes(1));
  }
  private static string GetUserKey(string username) => $"User:Username:{username}";

  private T? GetItem<T>(object key) => _cache.TryGetValue(key, out T? value) ? value : default;
  private void SetItem(object key, object value, TimeSpan expiration) => _cache.Set(key, value, expiration);
}
