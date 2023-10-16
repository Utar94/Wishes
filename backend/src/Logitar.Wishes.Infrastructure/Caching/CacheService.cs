using Logitar.EventSourcing;
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

  private T? GetItem<T>(object key) => _cache.TryGetValue(key, out T? value) ? value : default;
  private void SetItem(object key, object value, TimeSpan expiration) => _cache.Set(key, value, expiration);
}
