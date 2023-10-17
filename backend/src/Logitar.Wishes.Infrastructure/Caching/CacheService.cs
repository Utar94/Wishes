using Logitar.EventSourcing;
using Logitar.Portal.Contracts.ApiKeys;
using Logitar.Portal.Contracts.Users;
using Logitar.Wishes.Application.Caching;
using Logitar.Wishes.Contracts.Actors;
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

  public ApiKey? GetApiKey(string xApiKey) => GetItem<ApiKey>(GetApiKeyKey(xApiKey));
  public void SetApiKey(string xApiKey, ApiKey apiKey) => SetItem(GetApiKeyKey(xApiKey), apiKey, TimeSpan.FromMinutes(1));
  private static string GetApiKeyKey(string xApiKey) => $"X-API-Key:{xApiKey}";

  public User? GetUser(string credentials) => GetItem<User>(GetUserKey(credentials));
  public void SetUser(string credentials, User user) => SetItem(GetUserKey(credentials), user, TimeSpan.FromMinutes(1));
  private static string GetUserKey(string credentials) => $"User:Credentials:{credentials}";

  private T? GetItem<T>(object key) => _cache.TryGetValue(key, out T? value) ? value : default;
  private void SetItem(object key, object value, TimeSpan expiration) => _cache.Set(key, value, expiration);
}
